using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Paging;
using BackendSocialApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendSocialApp.Persistence.Repositories
{
    public class CoffeeFortuneTellingRepository : BaseRepository, ICoffeeFortuneTellingRepository
    {
        public CoffeeFortuneTellingRepository(AppDbContext context) : base(context) { }

        public async Task AddCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling)
        {
            await _context.CoffeeFortuneTellings.AddAsync(coffeeFortuneTelling);
        }

        public async Task AddCoffeeFortuneTellingPictureAsync(CoffeeFortuneTellingPicture coffeeFortuneTellingPicture)
        {
            await _context.CoffeeFortuneTellingPictures.AddAsync(coffeeFortuneTellingPicture);
        }

        public CoffeeFortuneTelling GetCoffeeFortuneTelling(Guid id)
        {
            return _context.CoffeeFortuneTellings.FirstOrDefault(a => a.Id == id);
        }

        public List<FortuneTellerUser> GetFortuneTellers()
        {
            return _context.FortuneTellerUsers.ToList();
        }

        public void UpdateCoffeeFortuneTelling(CoffeeFortuneTelling coffeeFortuneTelling)
        {
            _context.CoffeeFortuneTellings.Update(coffeeFortuneTelling);
        }

        public Task<IPagedList<CoffeeFortuneTelling>> GetUserItemsAsync(PageSearchArgs args, Guid userId)
        {
            IQueryable<CoffeeFortuneTelling> query = _context.CoffeeFortuneTellings;

            var orderByList = new List<Tuple<SortingOption, Expression<Func<CoffeeFortuneTelling, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "SubmitDate":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<CoffeeFortuneTelling, object>>>(sortingOption, c => c.SubmitDateUtc));
                            break;
                        case "SubmitDateByFortuneTeller":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<CoffeeFortuneTelling, object>>>(sortingOption, c => c.SubmitByFortuneTellerDateUtc));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<CoffeeFortuneTelling, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.Id));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>>();

            var userFilter = new FilteringOption
            {
                Field = "",
                Operator = FilteringOption.FilteringOperator.EQ,
                Value = userId
            };

            filterList.Add(new Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>(userFilter, c => c.User.Id == (Guid)userFilter.Value));

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>(filteringOption, c => c.Id == (Guid)filteringOption.Value));
                            break;
                        case "fortunetellerid":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>(filteringOption, c => c.FortuneTeller.Id == (Guid)filteringOption.Value));
                            break;
                        case "status":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>(filteringOption, c => c.Status == (CoffeeFortuneTellingStatus)filteringOption.Value));
                            break;
                    }
                }
            }

            var categoryPagedList = new PagedList<CoffeeFortuneTelling>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<CoffeeFortuneTelling>>(categoryPagedList);
        }

        public Task<IPagedList<CoffeeFortuneTelling>> GetFortuneTellerItemsAsync(PageSearchArgs args, Guid userId)
        {
            IQueryable<CoffeeFortuneTelling> query = _context.CoffeeFortuneTellings;

            var orderByList = new List<Tuple<SortingOption, Expression<Func<CoffeeFortuneTelling, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "SubmitDate":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<CoffeeFortuneTelling, object>>>(sortingOption, c => c.SubmitDateUtc));
                            break;
                        case "SubmitDateByFortuneTeller":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<CoffeeFortuneTelling, object>>>(sortingOption, c => c.SubmitByFortuneTellerDateUtc));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<CoffeeFortuneTelling, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.Id));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>>();

            var userFilter = new FilteringOption
            {
                Field = "",
                Operator = FilteringOption.FilteringOperator.EQ,
                Value = userId
            };

            filterList.Add(new Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>(userFilter, c => c.FortuneTeller.Id == (Guid)userFilter.Value));

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>(filteringOption, c => c.Id == (Guid)filteringOption.Value));
                            break;
                        case "userid":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>(filteringOption, c => c.User.Id == (Guid)filteringOption.Value));
                            break;
                        case "status":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<CoffeeFortuneTelling, bool>>>(filteringOption, c => c.Status == (CoffeeFortuneTellingStatus)filteringOption.Value));
                            break;
                    }
                }
            }

            var categoryPagedList = new PagedList<CoffeeFortuneTelling>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<CoffeeFortuneTelling>>(categoryPagedList);
        }
    }
}
