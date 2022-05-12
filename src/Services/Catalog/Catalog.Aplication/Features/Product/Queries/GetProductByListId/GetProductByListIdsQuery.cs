using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Aplication.Features.Product.Queries.GetProductByListId
{
    public class GetProductByListIdsQuery : IRequest<List<ProductsVm>>
    {
        public List<string> ItemsId { get; set; }
        public GetProductByListIdsQuery(List<string> itemsId)
        {
            ItemsId = itemsId ?? new();
        }
    }
}
