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
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.AssesmentScore>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.AssesmentScore)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetReligion(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.Religion>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.Religion)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetEthnic(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.Ethnic>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.Ethnic)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetEducation(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.Education>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.Education)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverType(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.DriverType>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.DriverType)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetClothesSize(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.ClothesSize>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.ClothesSize)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverClass(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.DriverClass>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.DriverClass)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverLicense(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.DriverLicense>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.DriverLicense)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverShift(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.DriverShift>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.DriverShift)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetDriverStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.DriverStatus>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.DriverStatus)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetGender(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.Gender>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.Gender)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetMaritalStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.MaritalStatus>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.MaritalStatus)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetReferralStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.ReferralStatus>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.ReferralStatus)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetServiceType(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.ServiceType>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.ServiceType)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }

        public override async Task<resEnumCommon> GetVehicleStatus(Empty request, ServerCallContext context)
        {
            var oRet = new EnumCommon();
            var ret = new resEnumCommon();
            var lAss = Common.Enums.EnumHelper.Enum2List<Common.Enums.VehicleStatus>();
            ArrayList arrayList = new ArrayList(lAss);
            for (int i = 0; i < lAss.Count; i++)
            {
                oRet = new EnumCommon();
                oRet.Id = i;
                oRet.Description = ((Common.Enums.VehicleStatus)i).ToString();
                ret.List.Add(oRet);
            }
            return ret;
        }
    }
}
