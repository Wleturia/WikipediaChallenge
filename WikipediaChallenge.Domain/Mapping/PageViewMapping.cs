using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikipediaChallenge.Domain.Mapping
{
    public class PageViewMapping
    {

        public List<VO.PageView> MapPageViewFromModelToDTOList(List<Entity.PageView> entities)
        {
            List<VO.PageView> list = new List<VO.PageView>();
            foreach (Entity.PageView entity in entities)
            {
                list.Add(MapPageViewFromModelToDTO(entity));
            }
            return list;
        }

        VO.PageView MapPageViewFromModelToDTO(Entity.PageView entity)
        {
            VO.PageView pageView = new(entity.domainCode, entity.pageTitle, entity.countViews);
            return pageView;
        }
    }
}
