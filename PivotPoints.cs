#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;
#endregion

// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    /// <summary>
    /// Enter the description of your new custom indicator here
    /// </summary>
    [Description("Enter the description of your new custom indicator here")]
    public class Pivot2 : Indicator
    {
        #region Variables
        // Wizard generated variables
        // User defined variables (add any user defined variables below)
        #endregion

        /// <summary>
        /// This method is used to configure the indicator and is called once before any bar data is loaded.
        /// </summary>
        protected override void Initialize()
        {
            Add(new Plot(Color.FromKnownColor(KnownColor.Orange), PlotStyle.Line, "P"));
            Add(new Plot(Color.FromKnownColor(KnownColor.Green), PlotStyle.Line, "R2"));
			Add(new Plot(Color.FromKnownColor(KnownColor.Yellow), PlotStyle.Line, "S2"));
			Add(new Line());
			Add(new Line());
			Add(new Line());
            Overlay				= true;
			
			//DrawOnPricePanel = true;
        }

        /// <summary>
        /// Called on each bar update event (incoming tick)
        /// </summary>
        protected override void OnBarUpdate()
        {
            // Use this method for calculating your indicator values. Assign a value to each
            // plot below by replacing 'Close[0]' with your own formula.
            double pivot = (High[0] + Low[0] + Close[0]) / 3;
			P.Set( pivot );
			Lines[0].Value = pivot;
			Lines[0].Name = "Pivot";
			
			double R2p = pivot + High[0] - Low[0];
            R2.Set( R2p );
			Lines[1].Value = R2p;
			Lines[1].Name = "R2";
			
			double S2p = pivot - High[0] + Low[0];
			S2.Set(S2p);
			Lines[2].Value = S2p;
			Lines[2].Name = "S2";
			
			
			
        }

        #region Properties
        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries P
        {
            get { return Values[0]; }
        }

        [Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries R2
        {
            get { return Values[1]; }
        }

		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries S2
        {
            get { return Values[2]; }
        }

		
		
		[Browsable(false)]	// this line prevents the data series from being displayed in the indicator properties dialog, do not remove
        [XmlIgnore()]		// this line ensures that the indicator can be saved/recovered as part of a chart template, do not remove
        public DataSeries PLine
        {
            get { return Values[3]; }
        }

        #endregion
		
    }
}

#region NinjaScript generated code. Neither change nor remove.
// This namespace holds all indicators and is required. Do not change it.
namespace NinjaTrader.Indicator
{
    public partial class Indicator : IndicatorBase
    {
        private Pivot2[] cachePivot2 = null;

        private static Pivot2 checkPivot2 = new Pivot2();

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Pivot2 Pivot2()
        {
            return Pivot2(Input);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Pivot2 Pivot2(Data.IDataSeries input)
        {
            if (cachePivot2 != null)
                for (int idx = 0; idx < cachePivot2.Length; idx++)
                    if (cachePivot2[idx].EqualsInput(input))
                        return cachePivot2[idx];

            lock (checkPivot2)
            {
                if (cachePivot2 != null)
                    for (int idx = 0; idx < cachePivot2.Length; idx++)
                        if (cachePivot2[idx].EqualsInput(input))
                            return cachePivot2[idx];

                Pivot2 indicator = new Pivot2();
                indicator.BarsRequired = BarsRequired;
                indicator.CalculateOnBarClose = CalculateOnBarClose;
#if NT7
                indicator.ForceMaximumBarsLookBack256 = ForceMaximumBarsLookBack256;
                indicator.MaximumBarsLookBack = MaximumBarsLookBack;
#endif
                indicator.Input = input;
                Indicators.Add(indicator);
                indicator.SetUp();

                Pivot2[] tmp = new Pivot2[cachePivot2 == null ? 1 : cachePivot2.Length + 1];
                if (cachePivot2 != null)
                    cachePivot2.CopyTo(tmp, 0);
                tmp[tmp.Length - 1] = indicator;
                cachePivot2 = tmp;
                return indicator;
            }
        }
    }
}

// This namespace holds all market analyzer column definitions and is required. Do not change it.
namespace NinjaTrader.MarketAnalyzer
{
    public partial class Column : ColumnBase
    {
        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.Pivot2 Pivot2()
        {
            return _indicator.Pivot2(Input);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Indicator.Pivot2 Pivot2(Data.IDataSeries input)
        {
            return _indicator.Pivot2(input);
        }
    }
}

// This namespace holds all strategies and is required. Do not change it.
namespace NinjaTrader.Strategy
{
    public partial class Strategy : StrategyBase
    {
        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        [Gui.Design.WizardCondition("Indicator")]
        public Indicator.Pivot2 Pivot2()
        {
            return _indicator.Pivot2(Input);
        }

        /// <summary>
        /// Enter the description of your new custom indicator here
        /// </summary>
        /// <returns></returns>
        public Indicator.Pivot2 Pivot2(Data.IDataSeries input)
        {
            if (InInitialize && input == null)
                throw new ArgumentException("You only can access an indicator with the default input/bar series from within the 'Initialize()' method");

            return _indicator.Pivot2(input);
        }
    }
}
#endregion
