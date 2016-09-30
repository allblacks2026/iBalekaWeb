using iBalekaWeb.Data.Infrastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.iBalekaAPI
{
    public interface IClubMemberClient
    {
        ListModelResponse<ClubMember> GetClubMembers(int clubId);
        SingleModelResponse<ClubMember> GetMember(int memberId);
        SingleModelResponse<ClubMember> DeRegisterMember(int memberId);
    }
    public class ClubMemberClient:ApiClient,IClubMemberClient
    {
        public const string ClubMemberUri = "ClubMember/";
        public ClubMemberClient() : base() { }

        public ListModelResponse<ClubMember> GetClubMembers(int clubId)
        {
            string getUrl = ClubMemberUri + "GetClubMembers?clubId=" + clubId;
            var clubs = GetListContent<ClubMember>(getUrl);
            return clubs;
        }
        public SingleModelResponse<ClubMember> GetMember(int memberId)
        {
            string getUrl = ClubMemberUri + "Member/GetClubMember?memberId=" + memberId;
            var club = GetSingleContent<ClubMember>(getUrl);
            return club;
        }
        public SingleModelResponse<ClubMember> DeRegisterMember(int memberId)
        {
            string getUrl = ClubMemberUri + "DeRegisterMember?clubmember=" + memberId;
            var clubs = DeleteContent<ClubMember>(getUrl,memberId);
            return clubs;
        }
    }
}
