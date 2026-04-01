namespace GraphQLDemo.Hellper
{
    public static class AIConstants
    {
        public const string MIDContext = @"
            The MID (Market Intelligence Dashboard) application is a comprehensive and centralized platform that brings together 
            multiple business-critical sections to provide a complete view of the data center market. It serves as a single source of truth for
            managing and analyzing both internal and external data related to supply, demand, pricing, capacity, and connectivity. 
            Through sections like Supply, Demand, and Capacity Summary, the application enables users to understand current and future market availability,
            utilization, and growth trends across regions. In addition to core market data, MID integrates several Power BI–driven analytics modules such as Double Digital,
            Market Metrics, Sales, and MBD, which help teams evaluate performance, track business development activities, and gain insights into service providers and 
            market dynamics. Features like Comps and Pricing allow detailed competitive analysis by comparing Digital Realty with other providers in terms of contracts,
            pricing strategies, and customer experience. The platform also provides deep visibility into cloud deployments, connectivity ecosystems, and internet exchanges,
            helping users understand how infrastructure and networks are evolving globally. Advanced mapping features such as SPMD Maps, MBD Maps, and Advanced Maps visually 
            represent data center locations, cloud services, and competitive landscapes, making analysis more intuitive and interactive. Furthermore, MID supports operational and 
            strategic functions through sections like Project Management, Usage Statistics, and GTM Apps, ensuring teams can monitor application usage, track projects, and align go-to-market strategies. 
            Overall, MID acts as an end-to-end intelligence and analytics platform that empowers organizations to make informed decisions, monitor competitors, optimize capacity, and drive business growth in the data center industry.";

        public const string AIInstructions = @"
            Analyze based on:

            - Performance
            - Errors / Failures
            - User Activity / Growth
            - Critical User Journeys
            - Trends (Over Time)
            - Anomalies (Unusual Behavior)
            - Business Impact
            - Severity / Priority
            - Recommendations (What to Do)
            ";
        }
}
