﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuFastPack.Querying;
using FubuLocalization;

namespace FubuFastPack.JqGrid
{
    /// <summary>
    /// The output model for rendering the actual grid?
    /// </summary>
    public class GridViewModel
    {
        private readonly IList<Criteria> _criterion = new List<Criteria>();

        public IEnumerable<Criteria> InitialCriteria()
        {
            return _criterion;
        }

        public void AddCriterion(IEnumerable<Criteria> criterion)
        {
            _criterion.AddRange(criterion);
        }

        public void AddCriteria(Criteria criteria)
        {
            _criterion.Add(criteria);
        }

        public void AddCriteria<T>(Expression<Func<T, object>> property, StringToken op, string value)
        {
            var criteria = Criteria.For(property, op.Key, value);
            AddCriteria(criteria);
        }

        public void ReplaceCriterion(IEnumerable<Criteria> criterion)
        {
            _criterion.Clear();
            _criterion.AddRange(criterion);

            GridModel.initialCriteria = criterion.ToArray();
        }



        public Type GridType { get; set; }
        public string GridName { get; set; }
        public JqGridModel GridModel { get; set; }
        public IEnumerable<FilteredProperty> FilteredProperties { get; set; }
        public bool CanSaveQuery { get; set; }
        public string HeaderText { get; set; }
        public string NewEntityUrl { get; set; }
        public string NewEntityText { get; set; }
        public bool AllowCreateNew { get; set; }
    }
}