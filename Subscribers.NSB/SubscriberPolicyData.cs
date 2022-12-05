using NServiceBus;


namespace Subscribers.NSB;

public class SubscriberPolicyData : ContainSagaData
{
    public int MeasureID { get; set; }
    public bool IsUpdateBMI { get; set; }
    public bool IsTracked { get; set; }
}

