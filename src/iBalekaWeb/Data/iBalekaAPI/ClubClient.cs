using iBalekaWeb.Data.Infrastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.iBalekaAPI
{
    public interface IClubClient
    {
        SingleModelResponse<Club> SaveClub(Club club);
        SingleModelResponse<Club> UpdateClub(Club club);
        SingleModelResponse<Club> GetClub(int clubId);
        ListModelResponse<Club> GetClubs();
        ListModelResponse<Club> GetUserClubs(string userId);
        SingleModelResponse<Club> DeleteClub(int clubId);

    }
    public class ClubClient:ApiClient,IClubClient
    {
        public const string ClubUri = "Club/";
        public ClubClient() : base(){ }

        public SingleModelResponse<Club> SaveClub(Club club)
        {
            string saveUrl = ClubUri + "CreateClub";
            var newClub = new Club
            {
                Name = club.Name,
                DateCreated = DateTime.Now.ToString(),
                Deleted= false,
                Description = club.Description,
                Location = club.Location,
                UserId = club.UserId
            };
            var createdClub = PostContent(saveUrl, newClub);
            return createdClub;
        }
        public SingleModelResponse<Club> UpdateClub(Club club)
        {
            string saveUrl = ClubUri + "Update/UpdateClub";
            var newClub = new Club
            {
                Name = club.Name,
                DateCreated = DateTime.Now.ToString(),
                Deleted = false,
                Description = club.Description,
                Location = club.Location,
                UserId = club.UserId
            };
            var updatedClub = PutContent(saveUrl, newClub);
            return updatedClub;
        }
        public SingleModelResponse<Club> GetClub(int clubId)
        {
            string getUrl = ClubUri + "GetClub?clubId=" + clubId;
            var model = GetSingleContent<Club>(getUrl);            
            return model;
        }
        public ListModelResponse<Club> GetClubs()
        {
            string getUrl = ClubUri + "GetAllClubs";
            var evnt = GetListContent<Club>(getUrl);
            return evnt;
        }
        public ListModelResponse<Club> GetUserClubs(string userId)
        {
            string getUrl = ClubUri + "User/GetUserClubs?userId=" + userId;
            var evnt = GetListContent<Club>(getUrl);
            return evnt;
        }
        public SingleModelResponse<Club> DeleteClub(int clubId)
        {
            string getUrl = ClubUri + "DeleteClub?club=" + clubId;
            var devnt = DeleteContent<Club>(getUrl, clubId);
            return devnt;
        }
    }
}
