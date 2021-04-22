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
        ReturnModel<object> Add(SlidersModel model);
        ReturnModel<object> Update(SlidersModel model);
        ReturnModel<object> SliderIsActiveUpdate(SlidersModel model);
        ReturnModel<object> Delete(int id);
        ReturnModel<object> DeleteAll();
    }
}
