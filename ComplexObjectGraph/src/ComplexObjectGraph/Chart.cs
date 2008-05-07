using System.Drawing;
using Iesi.Collections.Generic;
using System;

namespace ComplexObjectGraph
{
    public class Chart
    {
        public virtual int Id { get; set; }
        public virtual Curve MainCurve { get; set; }
    }

    public class Curve
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<Segment> Segments { get; set; }

        public Curve()
        {
            Segments = new HashedSet<Segment>();
        }
    }

    public class Segment
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual SegmentData SegmentData { get; set; }
        public virtual PlotDetails PlotDetails { get; set; }
        public virtual Transition Transition { get; set; }
        public virtual ISet<SegmentPoint> Points { get; set; }

        public Segment()
        {
            Points = new HashedSet<SegmentPoint>();
        }
    }

    public class SegmentPoint
    {
        public virtual int Id { get; set; }
        public virtual DataPoint DataPoint { get; set; }
    }

    public class DataPoint
    {
        public virtual int Id { get; set; }
        public virtual double X { get; set; }
        public virtual double Y { get; set; }
    }

    public class Transition
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual PlotDetails PlotDetails { get; set; }
        public virtual ISet<TransitionPoint> Points { get; set; }

        public Transition()
        {
            Points = new HashedSet<TransitionPoint>();
        }
    }

    public class TransitionPoint
    {
        public virtual int Id { get; set; }
        public virtual DataPoint DataPoint { get; set; }
    }

    public class PlotDetails
    {
        public virtual int Id { get; set; }
        public virtual Color LineColor { get; set; }
    }

    public class SegmentData
    {
        public virtual int Id { get; set; }
        public virtual string Data { get; set; }
    }
}
