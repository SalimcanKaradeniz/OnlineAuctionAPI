using OnlineAuction.Data.Model;
using OnlineAuction.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineAuction.Services.Sliders
{
    public interface ISliderService
    {
        List<OnlineAuction.Data.DbEntity.Sliders> GetSliders();
        List<OnlineAuction.Data.DbEntity.Sliders> GetActiveSliders();
        OnlineAuction.Data.DbEntity.Sliders GetSliderById(int id);
        ReturnModel<object> Add(SliderRequestModel model);
        ReturnModel<object> Update(SliderRequestModel model);
        ReturnModel<object> SliderIsActiveUpdate(SliderRequestModel model);
        ReturnModel<object> Delete(int id);
        ReturnModel<object> DeleteAll();
    }
}
