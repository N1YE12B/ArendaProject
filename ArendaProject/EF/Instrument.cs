//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArendaProject.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Instrument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Instrument()
        {
            this.Rent = new HashSet<Rent>();
        }
    
        public int idInstrument { get; set; }
        public string InstrumentName { get; set; }
        public bool InUse { get; set; }
        public int idTypeProduct { get; set; }
        public decimal Price { get; set; }
        public System.DateTime InstrumentLifeStart { get; set; }
        public System.DateTime InstrumentLifeDead { get; set; }
        public byte[] Photo { get; set; }
    
        public virtual TypeProduct TypeProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rent> Rent { get; set; }
    }
}
