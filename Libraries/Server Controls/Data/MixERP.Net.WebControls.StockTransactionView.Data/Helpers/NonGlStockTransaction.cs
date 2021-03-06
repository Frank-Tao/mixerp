﻿/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Entities;
using MixERP.Net.Entities.Transactions;

namespace MixERP.Net.WebControls.StockTransactionViewFactory.Data.Helpers
{
    public static class NonGlStockTransaction
    {
        public static bool AreSalesOrdersAlreadyMerged(Collection<long> ids)
        {
            if (ids == null)
            {
                return false;
            }

            const string sql = @"SELECT transactions.are_sales_orders_already_merged(@0);";
            return Factory.Scalar<bool>(sql, ids);
        }

        public static bool AreSalesQuotationsAlreadyMerged(Collection<long> ids)
        {
            if (ids == null)
            {
                return false;
            }
            const string sql = @"SELECT transactions.are_sales_quotations_already_merged(@0);";
            return Factory.Scalar<bool>(sql, ids);
        }

        public static bool ContainsIncompatibleTaxes(Collection<long> ids)
        {
            if (ids == null)
            {
                return false;
            }

            const string sql = @"SELECT transactions.contains_incompatible_taxes(@0);";
            return Factory.Scalar<bool>(sql, ids);
        }

        public static IEnumerable<DbGetNonGlProductViewResult> GetView(string book, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return GetView(CurrentSession.GetUserId(), book, CurrentSession.GetOfficeId(), dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }

        public static bool TransactionIdsBelongToSameParty(Collection<long> ids)
        {
            if (ids == null)
            {
                return false;
            }

            //Get the count of distinct number of parties associated with the transaction id.
            const string sql = "SELECT COUNT(DISTINCT party_id) FROM transactions.non_gl_stock_master WHERE non_gl_stock_master_id IN(@0);";
            return Factory.Scalar<int>(sql, ids).Equals(1);
        }

        private static IEnumerable<DbGetNonGlProductViewResult> GetView(int userId, string book, int officeId, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return Factory.Get<DbGetNonGlProductViewResult>("SELECT * FROM transactions.get_non_gl_product_view(@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10);", userId, book, officeId, dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }
    }
}