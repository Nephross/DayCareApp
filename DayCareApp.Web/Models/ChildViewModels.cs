using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DayCareApp.Data.DAL;

namespace DayCareApp.Web.Models
{
    public class CheckInPictures : IEnumerable
    {

        IList<Child> Children { get; set; }
        public IEnumerator GetEnumerator()
        {
            yield return Children;
        }
    }
}