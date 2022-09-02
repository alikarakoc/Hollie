using api.Helpers;
using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using AutoMapper;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : Controller
    {
      
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ContractController(Context _context, IMapper mapper)
        {
            this._context = _context;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("AllContract")]
        public async Task<ActionResponse<List<Contract>>> GetAllContract()
        {
            ActionResponse<List<Contract>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var contract = _context.Contracts;
            if (contract != null && contract.Count() > 0)
            {
                actionResponse.Data = _context.Contracts.Where(x => x.Status == true).ToList();
            }
            return actionResponse;

        }

       

        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Contract>> addContract([FromBody] ContractDto contractdto)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            int checkCode = _context.Contracts.Where(c => c.Code == contractdto.Code).Count();

            if (checkCode < 1)
            {
                
                Contract contract = new Contract();
                contract = _mapper.Map<Contract>(contractdto);

                contract.EnteredDate = TimeZoneInfo.ConvertTimeFromUtc(contract.EnteredDate, TimeZoneInfo.Local);
                contract.ExitDate = TimeZoneInfo.ConvertTimeFromUtc(contract.ExitDate, TimeZoneInfo.Local);
                contract.CreatedUser = contractdto.CreatedUser;
                contract.CreatedDate = DateTime.Now;
                _context.Contracts.Add(contract);
                contract.Status = true;
                

                _context.SaveChanges();

                ContractAgencyHelper.AddAgencies(contract.Id, contract.AgencyList, _context);

                ContractBoardHelper.AddBoards(contract.Id, contract.BoardList, _context);

                ContractRoomTypeHelper.AddRoomTypes(contract.Id, contract.RoomTypeList, _context);

                ContractMarketHelper.AddMarkets(contract.Id, contract.MarketList, _context);

                _context.SaveChanges();


            }
            return actionResponse;
        }


        [HttpGet]
        [Route("getById")]
        public async Task<ActionResponse<Contract>> GetHotelCategoryByID([FromQuery] ContractDto model)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var contract = await _context.Contracts.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (contract != null)
            {
                actionResponse.Data = contract;
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Contract>> DeleteHotelCategory([FromBody] ContractDto model)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            Contract contract = await _context.Contracts.FirstOrDefaultAsync(h => h.Id == model.Id);
            contract.AgencyList = _context.CAgencies.Where(c => c.ListId == model.Id).ToList();
            contract.BoardList = _context.CBoards.Where(c => c.ListId == model.Id).ToList();
            contract.RoomTypeList = _context.CRoomTypes.Where(c => c.ListId == model.Id).ToList();
            contract.MarketList = _context.CMarkets.Where(c => c.ListId == model.Id).ToList();

            ContractAgencyHelper.DeleteAgencies(contract.AgencyList, _context);

            ContractBoardHelper.DeleteBoards(contract.BoardList, _context);

            ContractRoomTypeHelper.DeleteRoomTypes(contract.RoomTypeList, _context);

            ContractMarketHelper.DeleteMarkets(contract.MarketList, _context);

            contract.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Contract>> UpdateHotelCategory([FromBody] ContractDto model)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Contract contract = await _context.Contracts.FirstOrDefaultAsync(h => h.Id == model.Id);

                contract.AgencyList =_context.CAgencies.Where(c => c.ListId == model.Id).ToList();
                contract.BoardList = _context.CBoards.Where(c => c.ListId == model.Id).ToList();
                contract.RoomTypeList = _context.CRoomTypes.Where(c => c.ListId == model.Id).ToList();
                contract.MarketList = _context.CMarkets.Where(c => c.ListId == model.Id).ToList();

                int checkCode = _context.Contracts.Where(h => h.Code == model.Code && h.Id!=model.Id).Count();
                if (checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }
                if (contract.Code == model.Code || checkCode == 0)
                {
                    contract.Code = model.Code;
                    contract.Name = model.Name;
                    contract.HotelId = model.HotelId;
                    contract.ADP = model.ADP;
                    contract.CH1 = model.CH1;
                    contract.CH2 = model.CH2;
                    contract.CH3 = model.CH3;
                    contract.CurrencyId = model.CurrencyId;
                    contract.UpdatedDate = DateTime.Now;
                    contract.UpdatedUser = model.UpdatedUser;
                    contract.Status = true;
                    contract.EnteredDate = TimeZoneInfo.ConvertTimeFromUtc(model.EnteredDate, TimeZoneInfo.Local);
                    contract.ExitDate = TimeZoneInfo.ConvertTimeFromUtc(model.ExitDate, TimeZoneInfo.Local);

                    
                    ContractAgencyHelper.DeleteAgencies(contract.AgencyList, _context);
                    ContractAgencyHelper.AddAgencies(model.Id, model.AgencyList, _context);

                    ContractBoardHelper.DeleteBoards(contract.BoardList, _context);
                    ContractBoardHelper.AddBoards(model.Id, model.BoardList, _context);

                    ContractRoomTypeHelper.DeleteRoomTypes(contract.RoomTypeList, _context);
                    ContractRoomTypeHelper.AddRoomTypes(model.Id, model.RoomTypeList, _context);

                    ContractMarketHelper.DeleteMarkets(contract.MarketList, _context);
                    ContractMarketHelper.AddMarkets(model.Id, model.MarketList, _context);



                    _context.SaveChanges();
                }
                return actionResponse;

            }
            catch (Exception ex)
            {
                actionResponse.ResponseType = ResponseType.Error;
                actionResponse.IsSuccessful = false;
                actionResponse.Errors.Add(ex.Message);
                return actionResponse;
            }

        }

        [HttpPost]
        [Route("searchContract")]
        public async Task<ActionResponse<List<Contract>>> searchContract([FromBody] SearchContractDto searchDto)
        {
           
            ActionResponse<List<Contract>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok
            };

            searchDto.BeginDate = searchDto.BeginDate.AddDays(1);
            searchDto.EndDate = searchDto.EndDate.AddDays(1);
            if (searchDto.BeginDate > searchDto.EndDate)
            {
                actionResponse.IsSuccessful = false;
                actionResponse.Message = "EndDate can not be smaller than BeginDate";
                return actionResponse;
            }

            
            List<Contract>  contracts =  _context.Contracts.Where(x=> 
            (searchDto.BeginDate <= x.EnteredDate &&  searchDto.EndDate >= x.ExitDate)||
            (x.EnteredDate <= searchDto.BeginDate && x.ExitDate <= searchDto.EndDate && !(x.ExitDate <= searchDto.BeginDate)) || 
            (x.EnteredDate >= searchDto.BeginDate && x.ExitDate >= searchDto.EndDate && x.EnteredDate <= searchDto.EndDate) ||
            (x.EnteredDate <= searchDto.BeginDate && x.ExitDate >= searchDto.EndDate)).ToList();
            actionResponse.Data = new List<Contract>(contracts);

            if (searchDto.Hotels != null)
            {
                contracts.Clear();
                foreach (var hotelID in searchDto.Hotels)
                {
                    List<Contract> contractsTemp = actionResponse.Data.Where(x => x.HotelId == hotelID).ToList();
                    contracts.AddRange(contractsTemp);
                }
                actionResponse.Data = contracts;    
               
            }
            actionResponse.IsSuccessful = true;

            return actionResponse;

        }

        [HttpPost]
        [Route("searchAccommodation")]
        public async Task<ActionResponse<List<PriceDto>>> searchAccommodation([FromBody] SearchContractDto searchDto)
        {
            List<PriceDto> testList = new List<PriceDto>();

            float minPrice;
            float childPrice1;
            float childPrice2;
            float childPrice3;
             

            ActionResponse<List<PriceDto>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                Data = testList

            };
            PriceDto priceDto = new PriceDto();
            //searchDto.Hotels.Add(searchDto.HotelId);
            //searchDto.BeginDate = searchDto.BeginDate.AddDays(1);
            searchDto.EndDate = searchDto.EndDate.AddDays(1);
            while (searchDto.BeginDate < searchDto.EndDate)
            {
                if (searchDto.BeginDate > searchDto.EndDate)
                {
                    actionResponse.IsSuccessful = false;
                    actionResponse.Message = "EndDate can not be smaller than BeginDate";
                    return actionResponse;
                }

                searchDto.BeginDate = searchDto.BeginDate.AddDays(1);
                List<Contract> contracts = _context.Contracts.Where(x =>
                (x.EnteredDate <= searchDto.BeginDate && x.ExitDate >= searchDto.BeginDate)).ToList();


                if (searchDto.Hotels != null)
                {
                    contracts = contracts.Where(x => searchDto.Hotels.Contains(x.HotelId)).ToList();
                    if (contracts.Count == 0)
                    {
                        return actionResponse;
                    }
                }

                contracts = contracts.Where(p => p.HotelId == searchDto.HotelId).ToList();
                Hotel hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == searchDto.HotelId);
                HotelFeature hotelFeature = await _context.HotelFeatures.FirstOrDefaultAsync(h => h.Id == hotel.HotelFeatureId);

                if (contracts.Count == 0)
                {
                    actionResponse.Message = "failed";
                    return actionResponse;
                }

                if (searchDto.HotelId == 0)
                {
                    actionResponse.Message = "failed";
                    return actionResponse;

                }

                childPrice1 = 0;
                childPrice2 = 0;
                childPrice3 = 0;
                minPrice = 9999999999999999;
                foreach (var contract in contracts)
                {

                    if (contract.ADP < minPrice)
                    {
                        minPrice = contract.ADP;

                        if (searchDto.child1Age < hotelFeature.BabyTop && searchDto.child1Age > 0)
                        {
                            childPrice1 = contract.CH1;
                        }
                        if (searchDto.child1Age > hotelFeature.BabyTop && searchDto.child1Age <= hotelFeature.ChildTop)
                        {
                            childPrice1 = contract.CH2;
                        }
                        if (searchDto.child1Age > hotelFeature.ChildTop && searchDto.child1Age <= hotelFeature.TeenTop)
                        {
                            childPrice1 = contract.CH3;
                        }
                        if (searchDto.child2Age < hotelFeature.BabyTop && searchDto.child2Age > 0)
                        {
                            childPrice2 = contract.CH1;
                        }
                        if (searchDto.child2Age > hotelFeature.BabyTop && searchDto.child2Age <= hotelFeature.ChildTop)
                        {
                            childPrice2 = contract.CH2;
                        }
                        if (searchDto.child2Age > hotelFeature.ChildTop && searchDto.child2Age <= hotelFeature.TeenTop)
                        {
                            childPrice2 = contract.CH3;
                        }
                        if (searchDto.child3Age < hotelFeature.BabyTop && searchDto.child3Age > 0)
                        {
                            childPrice3 = contract.CH1;
                        }
                        if (searchDto.child3Age > hotelFeature.BabyTop && searchDto.child3Age <= hotelFeature.ChildTop)
                        {
                            childPrice3 = contract.CH2;
                        }
                        if (searchDto.child3Age > hotelFeature.ChildTop && searchDto.child3Age <= hotelFeature.TeenTop)
                        {
                            childPrice3 = contract.CH3;
                        }
                       
                    }

                }

                
                priceDto.totalPrice = (minPrice + childPrice1 + childPrice2 + childPrice3 + priceDto.totalPrice) * searchDto.adult;
                //actionResponse.Data = priceDto;
                actionResponse.IsSuccessful = true;
            }

            //priceDto.totalPrice = priceDto.totalPrice * searchDto.adult;
            priceDto.startDate = searchDto.BeginDate;
            priceDto.endDate = searchDto.EndDate;

            //testList.Add(priceDto);
            actionResponse.Data.Add(priceDto);

            return actionResponse;
        }


        [HttpPost]
        [Route("detailAccommodation")]
        public async Task<ActionResponse<List<PriceSearchDto>>> detailAccommodation([FromBody] SearchAccommodationDate searchDto)
        {

            ActionResponse<List<PriceSearchDto>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                Data = new List<PriceSearchDto>()
                
            };

            if (searchDto.BeginDate > searchDto.EndDate)
            {
                actionResponse.IsSuccessful = false;
                actionResponse.Message = "EndDate can not be smaller than BeginDate";
                return actionResponse;
            }


            List<Contract> contracts = _context.Contracts.Where(x => x.Status == true &&
            (searchDto.BeginDate <= x.EnteredDate && searchDto.EndDate >= x.ExitDate) ||
            (x.EnteredDate <= searchDto.BeginDate && x.ExitDate <= searchDto.EndDate && !(x.ExitDate <= searchDto.BeginDate)) ||
            (x.EnteredDate >= searchDto.BeginDate && x.ExitDate >= searchDto.EndDate && x.EnteredDate <= searchDto.EndDate) ||
            (x.EnteredDate <= searchDto.BeginDate && x.ExitDate >= searchDto.EndDate)).ToList();
            

            if (searchDto.Hotels != null)
            {
                contracts = contracts.Where(x => searchDto.Hotels.Contains(x.HotelId)).ToList();
            }

            List<PriceSearchDto> dtoList = new List<PriceSearchDto>();


            contracts.ForEach( x =>
            {
                x.AgencyList = _context.CAgencies.Where(p => p.ListId == x.Id).ToList();
                x.RoomTypeList = _context.CRoomTypes.Where(p => p.ListId == x.Id).ToList();
            });

            List<int> agencyIds = new List<int>();
            contracts.ForEach(x => {
                agencyIds.AddRange(x.AgencyList.Select(s => s.AgencyId));
            });

            agencyIds=agencyIds.Distinct().ToList();

            var agencies = _context.Agencies.ToList();
            var roomTypes=_context.RoomTypes.ToList();
            var hotels = _context.Hotels.ToList();

            TimeSpan ts = (TimeSpan)(searchDto.EndDate - searchDto.BeginDate);
            int days = (int)ts.TotalDays;

            foreach(int i in agencyIds)
            {
                Agency agency=agencies.FirstOrDefault(x => x.Id==i);

                var tempCont = contracts.Where(p => p.AgencyList.Any(x => x.AgencyId == i)).ToList();

                List<int> roomIds = new List<int>();
                tempCont.ForEach(x => {
                    roomIds.AddRange(x.RoomTypeList.Select(s => s.RoomTypeId));
                });

                foreach(int r in roomIds)
                {
                    RoomType roomType = roomTypes.FirstOrDefault(x => x.Id==r);
                    var finalConts=tempCont.Where(x => x.RoomTypeList.Any(t=> t.RoomTypeId==r)).ToList();

                    PriceSearchDto dto = new PriceSearchDto();

                    dto.Adult = searchDto.Adult;
                    dto.AgencyId = i;
                    dto.AgencyName = agency.Name;
                    dto.NumberOfChild = searchDto.NumberOfChild;
                    dto.ChildAges = new int[] { searchDto.Child1Age, searchDto.Child2Age, searchDto.Child3Age};
                    dto.HotelId = finalConts[0].HotelId;
                    dto.HotelName = hotels.Where(p => p.Id == dto.HotelId).Select(p => p.Name).FirstOrDefault();
                    dto.RoomTypeId = roomType.Id;
                    dto.RoomName = roomTypes.Where(p => p.Id == dto.RoomTypeId).Select(p => p.Name).FirstOrDefault();

                    bool extractThis = false;

                    for (int t = 0; t < days; t++)
                    {
                        DateTime staydate = searchDto.BeginDate.AddDays(t);

                        var datCont=finalConts.Where(h=> h.EnteredDate<=staydate && h.ExitDate>=staydate).OrderBy(o=> o.ADP).FirstOrDefault();
                        if (datCont != null)
                        {
                            Hotel hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == datCont.HotelId);
                            HotelFeature hotelFeature = await _context.HotelFeatures.FirstOrDefaultAsync(h => h.Id == hotel.HotelFeatureId);

                            PriceSearchDetail detail = new PriceSearchDetail();
                            detail.StayDay = t + 1;
                            detail.ContractId = datCont.Id;
                            detail.ContractCode = datCont.Code;
                            detail.BasePrice = datCont.ADP * dto.Adult;
                            for (int a = 0; a < 3; a++)
                            {
                                if (dto.ChildAges[a] == 0)
                                {

                                }
                                else if (dto.ChildAges[a] <= hotelFeature.BabyTop)
                                {
                                    detail.BasePrice += datCont.CH1;
                                }
                                else if (dto.ChildAges[a] <= hotelFeature.ChildTop)
                                {
                                    detail.BasePrice += datCont.CH2;
                                }
                                else if (dto.ChildAges[a] <= hotelFeature.TeenTop)
                                {
                                    detail.BasePrice += datCont.CH3;
                                }
                                else
                                {
                                    detail.BasePrice += datCont.ADP;
                                }
                            }

                            detail.NetPrice = detail.BasePrice;

                            dto.PriceDetails.Add(detail);

                        

                        }
                        else
                        {
                            extractThis = true;
                            break;
                        }
                        foreach (var price in dto.PriceDetails)
                        {
                            dto.TotalPrice = dto.TotalPrice + price.NetPrice;
                        }
                    
                    
                    }
                    if (extractThis) continue;
                    dtoList.Add(dto);
                }
            }
            actionResponse.Data = dtoList;
            actionResponse.IsSuccessful = true;

            return actionResponse;

        }


    }

}
