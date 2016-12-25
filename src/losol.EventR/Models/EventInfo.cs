using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace losol.EventR.Models
{
    public class EventInfo
    {
        public int Id { get; set; }

        [Display(Name = "Tittel på kurset")]
        public string Name { get; set; }

        [Display(Name = "Kort beskrivelse av kurset")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Klart til publisering?")]
        public bool Publish { get; set; }

        [Display(Name = "Hvor er kurset?")]
        public string Location { get; set; }

        [Display(Name = "Fra dato")]
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Til dato")]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Påmeldingsfrist")]
        [DataType(DataType.Date)]
        public DateTime? LastEnrolmentDate { get; set; } //påmeldingsfrist

        [Display(Name = "Avmeldingsfrist")]
        [DataType(DataType.Date)]
        public DateTime? LastWithdrawalDate { get; set; } //avmeldingsfrist

        [Display(Name = "Antall deltakere")]
        public int MaxAttendees { get; set; } = 0; //maks antall deltakere

        [Display(Name = "Pris")]
        public decimal Price { get; set; }

        [Display(Name = "Mva-sats")]
        public decimal VatPercent { get; set; } = 0;  //ie0% or 25%

        [Display(Name = "Mer informasjon")]
        [DataType(DataType.MultilineText)]
        public string MoreInformation { get; set; }

        [Display(Name = "Velkomstbrev")]
        [DataType(DataType.MultilineText)]
        public string WelcomeLetter { get; set; } //Text to send to all attendees

        [Display(Name = "Diplomtekst")]
        [DataType(DataType.MultilineText)]
        public string DiplomaDescription { get; set; } //Text to present on the Diploma


        // Add code for attendees here
    }
}
