using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.Responses
{
    //interfaces
    public interface IResponse
    {
        String Message { get; set; }
        Boolean DidError { get; set; }
        String ErrorMessage { get; set; }
    }
    public interface IListModelResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
    public interface ISingleModelResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }

    //models
    public class ListModelResponse<TModel> : IListModelResponse<TModel>
    {
        public String Message { get; set; }
        public Boolean DidError { get; set; }
        public String ErrorMessage { get; set; }
        public IEnumerable<TModel> Model { get; set; }
    }
    public class SingleModelResponse<TModel> : ISingleModelResponse<TModel>
    {
        public String Message { get; set; }
        public Boolean DidError { get; set; }
        public String ErrorMessage { get; set; }
        public TModel Model { get; set; }
    }
}
