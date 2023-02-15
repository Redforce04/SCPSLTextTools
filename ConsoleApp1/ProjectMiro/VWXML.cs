using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ConsoleApp1;

namespace ProjectMiro
{
    public class VWXMLParser
    {
		public static SLData ParseXML(string fileLoc)
		{
			if (!File.Exists(fileLoc))
			{
				throw new FileNotFoundException($"Could not find file \'{fileLoc}\' while trying to parse xml.");
			}
			string xml = File.ReadAllText(fileLoc);
			Regex replaceAccessories = new Regex(
				@"<Accessories>[\s]*?" +     // Find opener: <Accessories>
				@"<UID_(?'ID'[\s\S]*?)>" +          // Find Accessory Tag: <UID_(ID)>
				@"(?'Data'[\s\S]*?)" +              // Find Data Contained: ex. (?'GroupName'InfoToCapture)
				@"</UID_\k<ID>>[\s]*?" +            // Find Closer Accessory Tag: <UID_(ID)>
				@"</Accessories>" +                 // Find Closer </Accessories>
				@"|<Accessories/>" +                // Alternative: <Accessories/>
				@"|<Accessories></Accessories>");  // Alternative: <Accessories></Accessories>
			string replaceAccessoriesString =
				@"<Accessories>\n        " +        // Opener <Accessories>
				@"<LightingAccessory> " +           // Accessory Opener: Now <LightingAccessory>
				@"${Data}" +                        // All the data it contains
				@"</LightingAccessory>" +           // Accessory Closer: Now </LightingAccessory>
				@"\n      </Accessories>";          // Closer </Accessories>


			Regex replaceInstruments = new Regex(
				@"<UID_(?'Id'[\s\S]*?)>" +   // UID Opener
					@"(?'Data'[\s\S]*?)" +          // Data
					@"</UID_\k<Id>>");             // UID Closer
			string replaceInstrumentsString =
				@"<LightingFixture>" +      // Opener 
				@"${Data}" +                // Data
				@"</LightingFixture>";      // Closer


			Regex replaceUniverse = new Regex(
				@"<(?'Escaped'\/)Universe[+\d].*?>" +
				@"|<(?'Escaped')Universe[+\d].*?>");
			string replaceUniverseString =
				@"<${Escaped}Universe>";

			Regex replaceSystem = new Regex(
				@"<System(?'Id'[\S])>");
			string replaceSystemString =
				$"<System>\n      " +
				@"<UniverseId>" +
				@"${Id}" +
				@"</UniverseId>";

			Regex replaceSystemCloser = new Regex(
				@"</System[\S]>");
			string replaceSystemCloserString =
				@"</System>";



			xml = replaceAccessories.Replace(xml, replaceAccessoriesString);
			xml = replaceInstruments.Replace(xml, replaceInstrumentsString);
			xml = replaceUniverse.Replace(xml, replaceUniverseString);
			xml = replaceSystem.Replace(xml, replaceSystemString);
			xml = replaceSystemCloser.Replace(xml, replaceSystemCloserString);
			xml = xml.Replace("True", "true", StringComparison.CurrentCulture);
			xml = xml.Replace("False", "false", StringComparison.CurrentCulture);

			XmlSerializer serializer = new XmlSerializer(typeof(SLData));

			//await File.WriteAllTextAsync($"{fileLoc}-new.xml", xml);
			using (StringReader reader = new StringReader(xml))
			{

				var test = (SLData)serializer.Deserialize(reader);
				return test;
			}

		}
		// using System.Xml.Serialization;
		// XmlSerializer serializer = new XmlSerializer(typeof(SLData));
		// using (StringReader reader = new StringReader(xml))
		// {
		//    var test = (SLData)serializer.Deserialize(reader);
		// }
	}

	[XmlRoot(ElementName = "ExportFieldList")]
	public class ExportFieldList
	{

		[XmlElement(ElementName = "AppStamp", IsNullable = true)]
		public string AppStamp { get; set; } = "";

        [XmlElement(ElementName = "TimeStamp")]
        public double TimeStamp { get; set; } = 0;

		[XmlElement(ElementName = "Absolute_Address", IsNullable = true)]
		public string AbsoluteAddress { get; set; } = "";

		[XmlElement(ElementName = "Focus", IsNullable = true)]
		public string Focus { get; set; } = "";

		[XmlElement(ElementName = "Inst_Type", IsNullable = true)]
		public string InstType { get; set; } = "";

		[XmlElement(ElementName = "Unit_Number", IsNullable = true)]
		public string UnitNumber { get; set; } = "";

		[XmlElement(ElementName = "Template2", IsNullable = true)]
		public string Template2 { get; set; } = "";

		[XmlElement(ElementName = "Template", IsNullable = true)]
		public string Template { get; set; } = "";

		[XmlElement(ElementName = "Color", IsNullable = true)]
		public string Color { get; set; } = "";

		[XmlElement(ElementName = "Circuit_Name", IsNullable = true)]
		public string CircuitName { get; set; } = "";

		[XmlElement(ElementName = "Circuit_Number", IsNullable = true)]
		public string CircuitNumber { get; set; } = "";

		[XmlElement(ElementName = "Dimmer", IsNullable = true)]
		public string Dimmer { get; set; } = "";

		[XmlElement(ElementName = "Channel")]
		public string Channel { get; set; } = "";

		[XmlElement(ElementName = "Position", IsNullable = true)]
		public string Position { get; set; } = "";

		[XmlElement(ElementName = "Wattage", IsNullable = true)]
		public string Wattage { get; set; } = "";

		[XmlElement(ElementName = "Purpose", IsNullable = true)]
		public string Purpose { get; set; } = "";

		[XmlElement(ElementName = "User_Field_1", IsNullable = true)]
        public string UserField1 { get; set; } = "";

        [XmlElement(ElementName = "User_Field_2", IsNullable = true)]
        public string UserField2 { get; set; } = "";

        [XmlElement(ElementName = "User_Field_3", IsNullable = true)]
        public string UserField3 { get; set; } = "";

		[XmlElement(ElementName = "User_Field_4", IsNullable = true)]
		public string UserField4 { get; set; } = "";

		[XmlElement(ElementName = "User_Field_5", IsNullable = true)]
		public string UserField5 { get; set; } = "";

		[XmlElement(ElementName = "User_Field_6", IsNullable = true)]
		public string UserField6 { get; set; } = "";

		[XmlElement(ElementName = "System", IsNullable = true)]
		public string System { get; set; } = "";

		[XmlElement(ElementName = "Mark", IsNullable = true)]
		public string Mark { get; set; } = "";
	}

	[XmlRoot(ElementName = "Accessories")]
	public class Accessories
    {

        [XmlElement(ElementName = "LightingAccessory")]
        public List<LightingAccessory> LightingAccessory { get; set; } = new List<LightingAccessory>();
    }

	[XmlRoot(ElementName = "LightingFixture")]
	public class LightingFixture
	{

		[XmlElement(ElementName = "Action", IsNullable = true)]
		public string Action { get; set; } = "";

		[XmlElement(ElementName = "TimeStamp")]
		public double TimeStamp { get; set; } = 0;

		[XmlElement(ElementName = "AppStamp", IsNullable = true)]
		public string AppStamp { get; set; } = "";

		[XmlElement(ElementName = "UID", IsNullable = true)]
		public string UID { get; set; } = "";

		[XmlElement(ElementName = "Lightwright_ID", IsNullable = true)]
		public string LightwrightID { get; set; } = "";

		[XmlElement(ElementName = "Use_Legend", IsNullable = true)]
		public string UseLegend { get; set; } = "";

		[XmlElement(ElementName = "Device_Type", IsNullable = true)]
		public string DeviceType { get; set; } = "";

		[XmlElement(ElementName = "Symbol_Name", IsNullable = true)]
		public string SymbolName { get; set; } = "";

		[XmlElement(ElementName = "Focus", IsNullable = true)]
		public object Focus { get; set; } = null;

		[XmlElement(ElementName = "Class", IsNullable = true)]
		public string Class { get; set; } = "";

		[XmlElement(ElementName = "Layer", IsNullable = true)]
		public string Layer { get; set; } = "";

		[XmlElement(ElementName = "Absolute_Address")]
		public int AbsoluteAddress { get; set; } = 0;

        [XmlElement(ElementName = "Inst_Type", IsNullable = true)]
        public string InstType { get; set; } = "";

        [XmlElement(ElementName = "Unit_Number", IsNullable = true)]
        public string UnitNumber { get; set; } = "";

		[XmlElement(ElementName = "Template2", IsNullable = true)]
		public object Template2 { get; set; } = null;

		[XmlElement(ElementName = "Template", IsNullable = true)]
		public object Template { get; set; } = null;

		[XmlElement(ElementName = "Color", IsNullable = true)]
        public string Color { get; set; } = "";

		[XmlElement(ElementName = "Circuit_Name", IsNullable = true)]
		public object CircuitName { get; set; } = null;

		[XmlElement(ElementName = "Circuit_Number", IsNullable = true)]
		public object CircuitNumber { get; set; } = null;

		[XmlElement(ElementName = "Dimmer", IsNullable = true)]
        public object Dimmer { get; set; } = null;

		[XmlElement(ElementName = "Channel", IsNullable = true)]
		public string Channel { get; set; } = "";

        [XmlElement(ElementName = "Position", IsNullable = true)]
        public string Position { get; set; } = "";

		[XmlElement(ElementName = "Wattage")]
		public string Wattage { get; set; } = "";

        [XmlElement(ElementName = "Purpose", IsNullable = true)]
        public string Purpose { get; set; } = "";

		[XmlElement(ElementName = "User_Field_1", IsNullable = true)]
		public object UserField1 { get; set; } = null;

		[XmlElement(ElementName = "User_Field_2", IsNullable = true)]
		public object UserField2 { get; set; } = null;

		[XmlElement(ElementName = "User_Field_3", IsNullable = true)]
		public object UserField3 { get; set; } = null;

		[XmlElement(ElementName = "User_Field_4", IsNullable = true)]
		public object UserField4 { get; set; } = null;

		[XmlElement(ElementName = "User_Field_5", IsNullable = true)]
		public object UserField5 { get; set; } = null;

		[XmlElement(ElementName = "User_Field_6", IsNullable = true)]
		public object UserField6 { get; set; } = null;

		[XmlElement(ElementName = "System", IsNullable = true)]
		public string System { get; set; } = "";

		[XmlElement(ElementName = "Mark", IsNullable = true)]
        public object Mark { get; set; } = null;

		[XmlElement(ElementName = "X_Location_mm")]
		public double XLocationMm { get; set; } = 0;

		[XmlElement(ElementName = "Y_Location_mm")]
		public double YLocationMm { get; set; } = 0;

		[XmlElement(ElementName = "Z_Location_mm")]
		public double ZLocationMm { get; set; } = 0;

		[XmlElement(ElementName = "Rotation")]
		public double Rotation { get; set; } = 0;

		[XmlElement(ElementName = "Accessories", IsNullable = true)]
		public Accessories Accessories { get; set; }
	}

	[XmlRoot(ElementName = "LightingAccessory")]
	public class LightingAccessory
	{

		[XmlElement(ElementName = "UID", IsNullable = true)]
		public string UID { get; set; } = "";

		[XmlElement(ElementName = "Device_Type", IsNullable = true)]
		public string DeviceType { get; set; } = "";

		[XmlElement(ElementName = "Symbol_Name", IsNullable = true)]
		public string SymbolName { get; set; } = "";

		[XmlElement(ElementName = "Inst_Type", IsNullable = true)]
		public string InstType { get; set; } = "";
	}

	[XmlRoot(ElementName = "InstrumentData")]
	public class InstrumentData
    {

        [XmlElement(ElementName = "Action", IsNullable = true)]
        public string Action { get; set; } = "";

        [XmlElement(ElementName = "AppStamp", IsNullable = true)]
        public string AppStamp { get; set; } = "";

        [XmlElement(ElementName = "VWVersion")]
        public int VWVersion { get; set; } = 0;

		[XmlElement(ElementName = "VWBuild")]
		public int VWBuild { get; set; } = 0;

        [XmlElement(ElementName = "AutoRot2D")]
        public bool AutoRot2D { get; set; } = false;

		[XmlElement(ElementName = "LightingFixture")]
		public List<LightingFixture> LightingFixture { get; set; } = new List<LightingFixture>();
	}

	[XmlRoot(ElementName = "Universe")]
	public class Universe
	{

		[XmlElement(ElementName = "Label")]
		public int Label { get; set; } = 0;

		[XmlElement(ElementName = "Start")]
		public int Start { get; set; } = 0;

		[XmlElement(ElementName = "End")]
		public int End { get; set; } = 0;
	}

	[XmlRoot(ElementName = "System")]
	public class System
    {

        [XmlElement(ElementName = "UniverseId", IsNullable = true)]
        public string UniverseId { get; set; } = "";

        [XmlElement(ElementName = "Universe", IsNullable = true)]
        public List<Universe> Universe { get; set; } = new List<Universe>();
    }

	[XmlRoot(ElementName = "UniverseSettings")]
	public class UniverseSettings
    {

        [XmlElement(ElementName = "AppStamp", IsNullable = true)]
        public string AppStamp { get; set; } = "";

        [XmlElement(ElementName = "UniverseEnabled")]
        public bool UniverseEnabled { get; set; } = true;

        [XmlElement(ElementName = "TimeStamp")]
        public double TimeStamp { get; set; } = 0;

        [XmlElement(ElementName = "VWVersion")]
        public int VWVersion { get; set; } = 0;

		[XmlElement(ElementName = "VWBuild")]
		public int VWBuild { get; set; } = 0;

		[XmlElement(ElementName = "System", IsNullable = true)]
		public List<System> System { get; set; } = new List<System>();
	}

	[XmlRoot(ElementName = "SLData")]
	public class SLData
	{

		[XmlElement(ElementName = "ExportFieldList", IsNullable = true)]
		public ExportFieldList ExportFieldList { get; set; }

		[XmlElement(ElementName = "InstrumentData")]
		public InstrumentData InstrumentData { get; set; } 

		[XmlElement(ElementName = "UniverseSettings", IsNullable = true)]
		public UniverseSettings UniverseSettings { get; set; }
	}
}
