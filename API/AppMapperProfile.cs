using API.DTOs;
using AutoMapper;
using Projects.Entities;

namespace API
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<Transaction, TransactionDTO>()
				.ForMember(n => n.Id, opt => opt.MapFrom(t => t.Id))
				.ForMember(n => n.Sum, opt => opt.MapFrom(t => t.Sum))
				.ForMember(n => n.TransactionType, opt => opt.MapFrom(t => t.TransactionTypeId == 1 ? "Income" : "Expendeture"))
				.ForMember(n => n.IncomeItemId, opt => opt.MapFrom(t => t.IncomeItemId))
				.ForMember(n => n.AccountId, opt => opt.MapFrom(t => t.IncomeItem.AccountId));
			CreateMap<IncomeItem, IncomeItemDTO>();
			CreateMap<Account, AccountDTO>();
		}
	}
}
