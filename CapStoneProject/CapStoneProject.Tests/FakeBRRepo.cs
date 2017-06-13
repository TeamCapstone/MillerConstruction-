using System.Collections.Generic;
using System.Linq;
using CapStoneProject.Models;
using Microsoft.EntityFrameworkCore;
using CapStoneProject.Repositories.Interfaces;
using System;
using Xunit;

namespace CapStoneProject.Repositories
{
    public class FakeBRRepo 
    {
        //public List<BidRequest> bidrequests = new List<BidRequest>();

        //public FakeBRRepo()
        //{
        //    UserIdentity user = new UserIdentity
        //    {
        //        FirstName = "Henry",LastName = "Homes",Email = "Hello1@gmail.com", Password = "Capstone1!", UserName = "Hello1@gmail.com"
        //    };
        //    BidRequest bidRequest = new BidRequest
        //    {
        //        User = user,Concrete = false,FrameWork = false,NewBuild = true,ProjectDescription = "Big deck",ProjectLocation = "Lane county", Remodel = false
        //    };
        //    bidrequests.Add(bidRequest);

        //    user = new UserIdentity
        //    {
        //        FirstName = "Harry",
        //        LastName = "Potter",
        //        Email = "hogworts@gmail.com",
        //        Password = "Capstone1!",
        //        UserName = "Hello1@gmail.com"
        //    };
        //    bidRequest = new BidRequest
        //    {
        //        User = user,
        //        Concrete = true,
        //        FrameWork = false,
        //        NewBuild = true,
        //        ProjectDescription = "Rebuild Griffindor tower",
        //        ProjectLocation = "Lane county",
        //        Remodel = false
        //    };
        //    bidrequests.Add(bidRequest);
        //}

        //public IQueryable<BidRequest> GetAllBidRequests()
        //{
        //    return bidrequests.AsQueryable();
        //}

        //public BidRequest GetBidRequestByID()
        //{
        //    int id = 0;
        //    return bidrequests.First(r => r.BidRequestID == id);
        //}

        //[Fact]
        //public void GetBidRequestByUserName()
        //{
        //    string name = "Potter";
        //    var br = bidrequests.First(n => n.User.LastName == name);
        //    Assert.Equal(br, bidrequests[1]);
        //}

        //[Fact]
        //public void Update(BidRequest req)
        //{
            
        //}

        //[Fact]
        //public void UniqueEmail()
        //{
        //    string email = "hogworts@gmail.com";
        //    BidRequest br = bidrequests.FirstOrDefault(r => r.User.Email == email);

        //    Assert.Equal(br.User.Email, "hogworts@gmail.com");
        //}

        //[Fact]
        //public void DeleteBR()
        //{
        //    var br = bidrequests[1];
        //    bidrequests.Remove(br);

        //    Assert.True(!bidrequests.Contains(br));
        //}
    }
}
