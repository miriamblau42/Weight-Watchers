using NServiceBus;


namespace Measure.NSB;

public class MeasurePolicyData : ContainSagaData
{
    public int MeasureId { get; set; }
    public bool IsMeasureAdded { get; set; }
    public bool IsTrackAdded { get; set; }
}

