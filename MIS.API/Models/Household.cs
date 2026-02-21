using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
// 
namespace MIS.API.Models;
//  4) Household / profile unit
// CREATE TABLE IF NOT EXISTS mp.household (
//     household_id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
//     submission_id uuid UNIQUE NOT NULL REFERENCES mp.submission(submission_id) ON DELETE CASCADE,
//     ward_no integer,
//     area text,
//     area_tole text,
//     tolename text,
//     tole text,
//     house_no text,
//     serialno text,
//     hh_no integer,
//     hh_id text,
//     location geography(Point,4326),
//     house_ownership text,
//     house_ownership_other_text text,
//     house_info_taken text,***
//     image_housemaster text,
//     image_house text,
//     sameperson text,***
//     nameinformant text,
//     datacollection_issues text,
//     -- convenience fields for public portal (optional; can be derived)
//     area_code text,
//     tole_code text,
//     tole_name_local text,
//     geom geography(Point,4326),
//     created_at timestamptz NOT NULL DEFAULT now()
// );

public class Household
{
    [Key]
    public Guid Id { get; set; }
    public Guid SubmissionId { get; set; }
    public Guid WardNo { get; set; }
    public string Area { get; set; } = null!;
    public string AreaTole { get; set; } = null!;
    public string ToleName { get; set; } = null!;
    public string HouseNo { get; set; } = null!;
    public string SerialNo { get; set; } = null!;
    public int HhNo { get; set; }
    public string HhId { get; set; } = null!;
    public Point Location { get; set; } = null!;
    public string HouseOwnership { get; set; } = null!;
    public string HouseOwnershipOtherText { get; set; } = null!;
    public string HouseInfoTaken { get; set; } = null!;
    public string ImageHouseMaster { get; set; } = null!;
    public string ImageHouse { get; set; } = null!;
    public string SamePerson { get; set; } = null!;
    public string NameInformant { get; set; } = null!;
    public string DataCollectionIssues { get; set; } = null!;

    // convenience fields for public portal (optional)
    public string AreaCode { get; set; } = null!;
    public string ToleCode { get; set; } = null!;
    public string ToleNameLocal { get; set; } = null!;
    public Point Geom { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Submission Submission { get; set; } = null!;

    public HouseInfo HouseInfo { get; set; } = null!;
}