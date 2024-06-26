using AutoMapper;
using Grpc.Core;
using WC.Service.Positions.Domain.Abstractions.Models;
using WC.Service.Positions.Domain.Abstractions.Services;

namespace WC.Service.Positions.gRPC.Server.Services;

public class GreeterPositionsService : GreeterPositions.GreeterPositionsBase
{
    private readonly IPositionProvider _provider;
    private readonly IMapper _mapper;

    public GreeterPositionsService(IPositionProvider provider, IMapper mapper)
    {
        _provider = provider;
        _mapper = mapper;
    }

    public override async Task<CheckPositionResponse> CheckPositionExists(CheckPositionRequest request,
        ServerCallContext context)
    {
        var result = await _provider.CheckPosition(_mapper.Map<PositionModel>(request));

        return new CheckPositionResponse
        {
            IsPositionExists = result
        };
    }
}