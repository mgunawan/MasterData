using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MasterData.Common2List.Protos;
using MongoDB.Bson.Serialization.Serializers;
using System.Collections.Generic;
using System.Collections;
using Common.Enums;

namespace MasterData.Services
{
    public class CommonService : Common2List.Protos.EnumCommon2ListService.EnumCommon2ListServiceBase
    {
        public override async Task<resEnumCommon> GetAssesment(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, AssesmentScore)> list = Common.Enums.EnumHelper.EnumToList<AssesmentScore>();
            foreach ((int index, AssesmentScore value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            //var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.AssesmentScore>();
            //ArrayList arrayList = new ArrayList(lAss);
            //for (int i = 0; i < lAss.Count; i++)
            //{
            //    oRet = new EnumCommon();
            //    oRet.Id = i;
            //    oRet.Description = ((Common.Enums.AssesmentScore)i).ToString();
            //    ret.List.Add(oRet);
            //}
            return ret;
        }

        public override async Task<resEnumCommon> GetReligion(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, Religion)> list = Common.Enums.EnumHelper.EnumToList<Religion>();
            foreach ((int index, Religion value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetEthnic(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, Ethnic)> list = Common.Enums.EnumHelper.EnumToList<Ethnic>();
            foreach ((int index, Ethnic value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetEducation(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, Education)> list = Common.Enums.EnumHelper.EnumToList<Education>();
            foreach ((int index, Education value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverType(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, DriverType)> list = Common.Enums.EnumHelper.EnumToList<DriverType>();
            foreach ((int index, DriverType value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetClothesSize(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, ClothesSize)> list = Common.Enums.EnumHelper.EnumToList<ClothesSize>();
            foreach ((int index, ClothesSize value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverClass(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, DriverClass)> list = Common.Enums.EnumHelper.EnumToList<DriverClass>();
            foreach ((int index, DriverClass value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverLicense(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, DriverLicense)> list = Common.Enums.EnumHelper.EnumToList<DriverLicense>();
            foreach ((int index, DriverLicense value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverShift(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, DriverShift)> list = Common.Enums.EnumHelper.EnumToList<DriverShift>();
            foreach ((int index, DriverShift value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, DriverStatus)> list = Common.Enums.EnumHelper.EnumToList<DriverStatus>();
            foreach ((int index, DriverStatus value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetGender(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, Gender)> list = Common.Enums.EnumHelper.EnumToList<Gender>();
            foreach ((int index, Gender value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetMaritalStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, MaritalStatus)> list = Common.Enums.EnumHelper.EnumToList<MaritalStatus>();
            foreach ((int index, MaritalStatus value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetReferralStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, ReferralStatus)> list = Common.Enums.EnumHelper.EnumToList<ReferralStatus>();
            foreach ((int index, ReferralStatus value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetServiceType(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, ServiceType)> list = Common.Enums.EnumHelper.EnumToList<ServiceType>();
            foreach ((int index, ServiceType value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetVehicleStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, VehicleStatus)> list = Common.Enums.EnumHelper.EnumToList<VehicleStatus>();
            foreach ((int index, VehicleStatus value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
            }
            return ret;
        }

        //most for vehicle
        public override async Task<resEnumCommon> GetIotStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            List<(int, IotStatus)> list = Common.Enums.EnumHelper.EnumToList<IotStatus>();
            foreach ((int index, IotStatus value) in list)
            {
                oRet = new EnumCommon();
                oRet.Id = (int)value;
                oRet.Description = value.GetDescription();
                ret.List.Add(oRet);
                //Console.WriteLine($"Index: {index}, Value: {value}, Description: {value.GetDescription()}");
            }
            return ret;
        }
    }
}
