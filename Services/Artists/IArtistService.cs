using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Artists
{
    public interface IArtistService
    {
        List<OnlineAuction.Data.DbEntity.Artists> GetArtists();
        List<OnlineAuction.Data.DbEntity.Artists> GetActiveArtists();
        OnlineAuction.Data.DbEntity.Artists GetArtistById(int id);
        ReturnModel<object> Add(ArtistsRequestModel model);
        ReturnModel<object> Update(ArtistsRequestModel model);
        ReturnModel<object> ArtistIsActiveUpdate(ArtistsRequestModel model);
        ReturnModel<object> Delete(int id);
        ReturnModel<object> DeleteAll();
    }
}
