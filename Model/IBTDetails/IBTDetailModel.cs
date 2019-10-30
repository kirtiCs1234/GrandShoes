﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class IBTDetailModel
    {
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> IbtBranchId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Z01 { get; set; }
        public Nullable<int> Z02 { get; set; }
        public Nullable<int> Z03 { get; set; }
        public Nullable<int> Z04 { get; set; }
        public Nullable<int> Z05 { get; set; }
        public Nullable<int> Z06 { get; set; }
        public Nullable<int> Z07 { get; set; }
        public Nullable<int> Z08 { get; set; }
        public Nullable<int> Z09 { get; set; }
        public Nullable<int> Z10 { get; set; }
        public Nullable<int> Z11 { get; set; }
        public Nullable<int> Z12 { get; set; }
        public Nullable<int> Z13 { get; set; }
        public Nullable<int> Z14 { get; set; }
        public Nullable<int> Z15 { get; set; }
        public Nullable<int> Z16 { get; set; }
        public Nullable<int> Z17 { get; set; }
        public Nullable<int> Z18 { get; set; }
        public Nullable<int> Z19 { get; set; }
        public Nullable<int> Z20 { get; set; }
        public Nullable<int> Z21 { get; set; }
        public Nullable<int> Z22 { get; set; }
        public Nullable<int> Z23 { get; set; }
        public Nullable<int> Z24 { get; set; }
        public Nullable<int> Z25 { get; set; }
        public Nullable<int> Z26 { get; set; }
        public Nullable<int> Z27 { get; set; }
        public Nullable<int> Z28 { get; set; }
        public Nullable<int> Z29 { get; set; }
        public Nullable<int> Z30 { get; set; }
        private int _ItemCount = 0;
        public int ItemCount
        {
            get
            {
                _ItemCount =
                    Convert.ToInt32((Z01 ?? 0) + (Z02 ?? 0) + (Z03 ?? 0) + (Z04 ?? 0) + (Z05 ?? 0) + (Z06 ?? 0) + (Z07 ?? 0) + (Z08 ?? 0) + (Z09 ?? 0) + (Z10 ?? 0) +
                    (Z11 ?? 0) + (Z12 ?? 0) + (Z13 ?? 0) + (Z14 ?? 0) + (Z15 ?? 0) + (Z16 ?? 0) +
                    (Z17 ?? 0) + (Z18 ?? 0) + (Z19 ?? 0) + (Z20 ?? 0) +
                    (Z21 ?? 0) + (Z22 ?? 0) + (Z23 ?? 0) + (Z24 ?? 0) + (Z25 ?? 0) + (Z26 ?? 0) + 
                    (Z27 ?? 0) + (Z28 ?? 0) + (Z29 ?? 0) + (Z30 ?? 0)
                    );

                return _ItemCount;
            }
        }
        public virtual IBTBranchModel IBTBranch { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}