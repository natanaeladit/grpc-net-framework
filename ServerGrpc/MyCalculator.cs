using Shared;
using System.Threading.Tasks;

namespace ServerGrpc
{
    public class MyCalculator : ICalculator
    {
        ValueTask<MultiplyResult> ICalculator.MultiplyAsync(MultiplyRequest request)
        {
            var result = new MultiplyResult { Result = request.X * request.Y };
            return new ValueTask<MultiplyResult>(result);
        }
    }
}
