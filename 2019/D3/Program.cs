﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace D3
{
    class Program
    {
        static void Main(string[] args)
        {

            var wire1 = new[]
            {
                // D3P1
                //"R75","D30","R83","U83","L12","D49","R71","U7","L72"
                //"R98","U47","R26","D63","R33","U87","L62","D20","R33","U53","R51"

                // D3P2
                //"R75","D30","R83","U83","L12","D49","R71","U7","L72"
                //"R98","U47","R26","D63","R33","U87","L62","D20","R33","U53","R51"

                "R999", "U626", "R854", "D200", "R696", "D464", "R54", "D246", "L359", "U57", "R994", "D813", "L889",
                "U238", "L165", "U970", "L773", "D904", "L693", "U512", "R126", "D421", "R732", "D441", "R453", "D349",
                "R874", "D931", "R103", "D794", "R934", "U326", "L433", "D593", "L984", "U376", "R947", "U479", "R533",
                "U418", "R117", "D395", "L553", "D647", "R931", "D665", "L176", "U591", "L346", "D199", "L855", "D324",
                "L474", "U251", "R492", "D567", "L97", "D936", "L683", "U192", "R198", "U706", "L339", "U66", "R726",
                "D102", "R274", "U351", "R653", "D602", "L695", "U921", "R890", "D654", "R981", "U351", "R15", "U672",
                "R856", "D319", "R102", "D234", "R248", "U169", "L863", "U375", "L412", "U75", "L511", "U298", "L303",
                "U448", "R445", "U638", "L351", "D312", "R768", "D303", "L999", "D409", "L746", "U266", "L16", "U415",
                "L951", "D763", "L976", "U342", "L505", "U770", "L228", "D396", "L992", "U3", "R243", "D794", "L496",
                "U611", "R587", "U772", "L306", "D119", "L470", "D490", "L336", "U518", "L734", "D654", "L150", "U581",
                "L874", "U691", "L243", "U94", "L9", "D582", "L402", "U563", "R468", "U96", "L311", "D10", "R232",
                "U762", "R630", "D1", "L674", "U685", "R240", "D907", "R394", "U703", "L64", "U397", "L810", "D272",
                "L996", "D954", "R797", "U789", "R790", "D526", "R103", "D367", "R143", "D41", "L539", "D735", "R51",
                "D172", "L33", "U241", "R814", "D981", "R748", "D699", "L716", "U647", "L381", "D351", "L381", "D121",
                "L52", "U601", "R515", "U713", "L404", "U45", "R362", "U670", "L235", "U102", "R373", "U966", "L362",
                "U218", "R280", "U951", "R371", "U378", "L10", "U670", "R958", "D423", "L740", "U888", "R235", "U899",
                "L387", "U167", "R392", "D19", "L330", "D916", "R766", "D471", "L708", "D83", "R749", "D696", "L50",
                "D159", "R828", "U479", "L980", "D613", "L182", "D875", "L307", "U472", "L317", "U999", "R435", "D364",
                "R737", "U550", "L233", "U190", "L501", "U610", "R433", "U470", "L801", "U52", "L393", "D596", "L378",
                "U220", "L967", "D807", "R357", "D179", "L731", "D54", "L804", "D865", "L994", "D151", "L181", "U239",
                "R794", "D378", "L487", "U408", "R817", "U809", "R678", "D599", "L564", "U480", "R525", "D189", "L641",
                "D771", "L514", "U72", "L248", "D334", "L859", "D318", "R590", "D571", "R453", "U732", "R911", "U632",
                "R992", "D80", "R490", "D234", "L710", "U816", "L585", "U180", "L399", "D238", "L103", "U605", "R993",
                "D539", "R330"
            };

            var wire2 = new[]
            {
                // D3P1
                //"U62","R66","U55","R34","D71","R55","D58","R83"
                //"U98","R91","D20","R16","D67","R40","U7","R15","U6","R7"

                // D3P2
                //"U62","R66","U55","R34","D71","R55","D58","R83"
                //"U98","R91","D20","R16","D67","R40","U7","R15","U6","R7"

                "L996", "U383", "L962", "U100", "L836", "D913", "R621", "U739", "R976", "D397", "L262", "D151", "L12",
                "U341", "R970", "U123", "L713", "U730", "L52", "D223", "L190", "D81", "R484", "D777", "R374", "U755",
                "R640", "D522", "R603", "D815", "R647", "U279", "R810", "U942", "R314", "D19", "L938", "U335", "R890",
                "U578", "R273", "U338", "R186", "D271", "L230", "U90", "R512", "U672", "R666", "D328", "L970", "U17",
                "R368", "D302", "L678", "D508", "L481", "U12", "L783", "D409", "L315", "D579", "L517", "D729", "R961",
                "D602", "R253", "D746", "R418", "D972", "R195", "D270", "L46", "D128", "L124", "U875", "R632", "D788",
                "L576", "U695", "R159", "U704", "R599", "D597", "R28", "D703", "L18", "D879", "L417", "U633", "L56",
                "U302", "R289", "U916", "R820", "D55", "R213", "U712", "R250", "D265", "L935", "D171", "L680", "U738",
                "L361", "D939", "R547", "D606", "L255", "U880", "R968", "U255", "R902", "D624", "L251", "U452", "L412",
                "D60", "L996", "D140", "L971", "U196", "R796", "D761", "L54", "U54", "L98", "D758", "L521", "U578",
                "L861", "U365", "L901", "D495", "L234", "D124", "L121", "D329", "L38", "U481", "L491", "D938", "L840",
                "D311", "L993", "D954", "R654", "U925", "L528", "D891", "L994", "D681", "L879", "D476", "L933", "U515",
                "L292", "U626", "R348", "D963", "L145", "U230", "L114", "D11", "R651", "D929", "R318", "D672", "R125",
                "D827", "L590", "U338", "L755", "D925", "L577", "D52", "R131", "D465", "R657", "D288", "R22", "D363",
                "R162", "D545", "L904", "D457", "R987", "D389", "L566", "D931", "L773", "D53", "R162", "U271", "L475",
                "U666", "L594", "U733", "R279", "D847", "R359", "U320", "R450", "D704", "L698", "D173", "R35", "D267",
                "L165", "D66", "L301", "U879", "R862", "U991", "R613", "D489", "L326", "D393", "R915", "U718", "R667",
                "U998", "R554", "U199", "R300", "U693", "R753", "U938", "R444", "U12", "L844", "D912", "R297", "D668",
                "R366", "U710", "L821", "U384", "R609", "D493", "R233", "U898", "R407", "U683", "R122", "U790", "L1",
                "U834", "L76", "U572", "R220", "U752", "L728", "D85", "L306", "D805", "R282", "U507", "R414", "D687",
                "L577", "U174", "L211", "U308", "L15", "U483", "R741", "D828", "L588", "D192", "L409", "D605", "L931",
                "U260", "L239", "D424", "L846", "U429", "L632", "U122", "L266", "D544", "R248", "U188", "R465", "U721",
                "R621", "U3", "L884", "U361", "L322", "U504", "R999", "U381", "R327", "U555", "L467", "D849", "R748",
                "U175", "R356"
            };

            var lineSegmentsWire1 = LineSegments.Build(wire1);
            var lineSegmentsWire2 = LineSegments.Build(wire2);

            var minimalDistance = int.MaxValue;
            var minimalSteps = int.MaxValue;

            var steps1 = 0;
            foreach (var lsw1 in lineSegmentsWire2)
            {
                var steps2 = 0;

                steps1 += lsw1.distance;

                foreach (var lsw2 in lineSegmentsWire1)
                {
                    steps2 += lsw2.distance;

                    var intersection = LineSegments.FindIntersection(lsw1, lsw2);
                    if (intersection == (0, 0)) continue;

                    var distance = Math.Abs(intersection.x) + Math.Abs(intersection.y);
                    if (distance < minimalDistance)
                    {
                        minimalDistance = distance;
                    }

                    // compensate for intersection
                    var steps = steps1 - lsw1.distance + steps2 - lsw2.distance
                        + LineSegments.Length(lsw1.startPoint, intersection)
                        + LineSegments.Length(lsw2.startPoint, intersection); 
                    if (steps < minimalSteps)
                    {
                        minimalSteps = steps;
                    }
                    

                    Console.WriteLine($"{steps1}:{steps2}={steps1+steps2} =>{distance}");
                }
            }

            //Console.WriteLine(minimalDistance); // D3P1: 399
            Console.WriteLine($"{minimalSteps}"); // D3P2: 
        }

    }

    class LineSegments
    {
        public static ((int x, int y) startPoint, (int x, int y) endPoint, int distance)[] Build(string[] wire)
        {
            var (x, y) = (0, 0);
            var lineSegments = new List<((int, int), (int, int), int)>();

            foreach (var wireSegment in wire)
            {
                var direction = wireSegment[0];
                var distance = int.Parse(wireSegment.Substring(1));

                switch (direction)
                {
                    case 'L':
                        lineSegments.Add(((x, y), (x -= distance, y), distance));
                        break;
                    case 'R':
                        lineSegments.Add(((x, y), (x += distance, y), distance));
                        break;
                    case 'U':
                        lineSegments.Add(((x, y), (x, y += distance), distance));
                        break;
                    case 'D':
                        lineSegments.Add(((x, y), (x, y -= distance), distance));
                        break;
                }
            }

            return lineSegments.ToArray();
        }

        public static (int x, int y) FindIntersection(
            ((int x, int y) startPoint, (int x, int y) endPoint, int distance) line1, 
            ((int x, int y) startPoint, (int x, int y) endPoint, int distance) line2
        )
        {
            // Get the segments' parameters.
            var dx12 = line1.endPoint.x - line1.startPoint.x;
            var dy12 = line1.endPoint.y - line1.startPoint.y;
            var dx34 = line2.endPoint.x - line2.startPoint.x;
            var dy34 = line2.endPoint.y - line2.startPoint.y;

            //// Solve for t1 and t2
            var denominator = dy12 * dx34 - dx12 * dy34;

            if (denominator == 0) return (0, 0);

            var t1 =
                ((line1.startPoint.x - line2.startPoint.x) * dy34 + (line2.startPoint.y - line1.startPoint.y) * dx34) / (float)denominator;

            if (float.IsInfinity(t1)) return (0, 0);

            var t2 =
                ((line2.startPoint.x - line1.startPoint.x) * dy12 + (line1.startPoint.y - line2.startPoint.y) * dx12)
                / (float)-denominator;

            return t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1 
                ? ((int)Math.Round(line1.startPoint.x + dx12 * t1), (int)Math.Round(line1.startPoint.y + dy12 * t1))
                : (0, 0);
        }

        public static int Length((int x, int y) startPoint, (int x, int y) endPoint)
        {
            return startPoint.x == endPoint.x
                ? Math.Abs(endPoint.y - startPoint.y)
                : Math.Abs(endPoint.x - startPoint.x);
        }
    }
}
