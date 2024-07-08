using Microsoft.EntityFrameworkCore;
using TreeTask.DTOs;
using TreeTask.Models;

namespace TreeTask.Service
{
    public class AssetsTreeService
    {
        private readonly AssetsContext _context;
        public AssetsTreeService(AssetsContext context)
        {
            _context = context;
        }

        // Return all nodes in the tree ordering by Parent Id
        public async Task<List<AssetsTreeDTO>> GetAllNodes()
        {
            
            var rootAssets = await _context.AssetsTrees
                .OrderBy(tree=> tree.ParentId)
                .Select(tree => new AssetsTreeDTO
                {
                    Id = tree.Id,
                    Name = tree.Name,
                    Money = tree.Money,
                    IsLast = tree.IsLast,
                    ParentId = tree.ParentId,
                    Number = tree.Number,
                    assetsTreeDTOs = null,
                }).ToListAsync();

            return await Task.FromResult(rootAssets);
        }

        //Build the tree and calculate the money of each branch to the root
        public async Task<List<AssetsTreeDTO>> GetAll()
        {
            var rootAssets = _context.AssetsTrees
                .Where(parent => parent.ParentId == null)
                .Select(parent => new AssetsTreeDTO
                {
                    Id = parent.Id,
                    Name = parent.Name,
                    Money = parent.Money,
                    IsLast = parent.IsLast,
                    ParentId = parent.ParentId,
                    Number = parent.Number,
                    assetsTreeDTOs = parent.assetsTrees.Select(child => new AssetsTreeDTO
                    {
                        Id = child.Id,
                        Name = child.Name,
                        Money = child.Money,
                        IsLast = child.IsLast,
                        ParentId = child.ParentId,
                        Number = child.Number,
                        assetsTreeDTOs = child.assetsTrees.Select(grandChild => new AssetsTreeDTO
                        {
                            Id = grandChild.Id,
                            Name = grandChild.Name,
                            Money = grandChild.Money,
                            IsLast = grandChild.IsLast,
                            ParentId = grandChild.ParentId,
                            Number = grandChild.Number,
                            assetsTreeDTOs = null
                        }).ToList()
                    }).ToList()
                }).ToList();

            // Calculate money for each parent
            CalculateMoney(rootAssets);

            return await Task.FromResult(rootAssets);
        }

        //Add node to the tree and check if the parent is last node or not
        public async Task<AssetsTree> Add(AssetsTreeCreateDTO dto)
        {
            if (dto.ParentId != null)
            {
                var assetById = await _context.AssetsTrees.FindAsync(dto.ParentId);
                if (assetById?.IsLast == true)
                    throw new Exception("لا يمكنك الإضافة لانه حساب نهائي");
                    //throw new Exception("Can't add on this asset cause it's final one");
            }

            var nodeNumber = GenerateNumber(dto);

            var asset = new AssetsTree()
            {
                Name = dto.Name,
                Money = dto.Money,
                IsLast = dto.IsLast,
                ParentId = dto.ParentId,
                Number = nodeNumber,
            };
            
            await _context.AssetsTrees.AddAsync(asset);
            await _context.SaveChangesAsync();

            return await Task.FromResult(asset);
        }

        //Calculate the money of each branch throug the tree using recursion 
        private static void CalculateMoney(IEnumerable<AssetsTreeDTO> assets)
        {
            foreach (var asset in assets)
            {
                if (asset.assetsTreeDTOs != null && asset.assetsTreeDTOs.Any())
                {
                    CalculateMoney(asset.assetsTreeDTOs);
                    asset.Money = asset.assetsTreeDTOs.Sum(child => child.Money);
                }
            }
        }

        //Generate the number of the node that will be added to the tree
        private string GenerateNumber(AssetsTreeCreateDTO assets)
        {
            if(assets.ParentId == null)
            {
                assets.Number = (_context.AssetsTrees.Where(a=> a.ParentId == null).Count()+1).ToString();
            }
            else
            {
                var parentNumber = _context.AssetsTrees.FirstOrDefault(a => a.Id == assets.ParentId)?.Number;
                var count = (_context.AssetsTrees.Where(a => a.ParentId == assets.ParentId).Count()+1).ToString();

                assets.Number = parentNumber+count;
            }

            return assets.Number;
        }
    }
}
