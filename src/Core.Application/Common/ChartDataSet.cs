namespace Core.Application.Common
{
    public class ChartDataSet
    {
        public string label { get; set; }
        public decimal[] data { get; set; }
        public string[] backgroundColor { get; set; } = new string[] {
            "#1AC3B3",
            "#008DF2",
            "#AA00FF",
            "#F50094",
            "#FF7924",
            "#FFA31F",
            "#FFD216",
            "#C7F461",
            "#04F196"
        };

        public string borderColor { get; set; } = "rgba(0, 0, 0, 0.1)";

        public static ChartDataSet[] FromDataArray(string label, decimal[] data,
            string borderColor = "rgba(0, 0, 0, 0.1)")
        {
            return new ChartDataSet[]
            {
                new ChartDataSet() {
                    label = label,
                    data = data,
                    borderColor = borderColor,
                }
            };
        }
    }
}
