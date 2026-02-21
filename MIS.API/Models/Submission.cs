using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace MIS.API.Models;

// - 3) Survey submission (one XLSForm submission)
// CREATE TABLE IF NOT EXISTS mp.submission (
//     submission_id uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
//     created_at timestamptz NOT NULL DEFAULT now(),
//     created_by uuid REFERENCES mp.app_user(user_id) ON DELETE SET NULL,
//     username text,
//     deviceid text,
//     phonenumber text,
//     email text,
//     start_geopoint geography(Point,4326),
//     today date,
//     p_code text,
//     d_code text,
//     lb_code text,

//     raw_submission_json jsonb
// );
public class Submission
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid CreatedById { get; set; }
    public string DeviceId { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public Point StartGeopoint { get; set; } = null!;

    public string PCode { get; set; } = null!;
    public string DCode { get; set; } = null!;
    public string LBCode { get; set; } = null!;

    public Dictionary<string, object>? RawSubmissionJson { get; set; }
    public AppUser? CreatedBy { get; set; }

    public ICollection<Household> Households { get; set; } = new List<Household>();
}