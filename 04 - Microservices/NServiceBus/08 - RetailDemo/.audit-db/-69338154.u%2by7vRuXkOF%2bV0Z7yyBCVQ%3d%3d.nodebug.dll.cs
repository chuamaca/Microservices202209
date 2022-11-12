using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;

public class Index_ExpiryKnownEndpointsIndex : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_ExpiryKnownEndpointsIndex()
	{
		this.ViewText = @"from knownEndpoint in docs.KnownEndpoints
select new {
	LastSeen = knownEndpoint.LastSeen.Ticks
}";
		this.ForEntityNames.Add("KnownEndpoints");
		this.AddMapDefinition(docs => 
			from knownEndpoint in ((IEnumerable<dynamic>)docs)
			where string.Equals(knownEndpoint["@metadata"]["Raven-Entity-Name"], "KnownEndpoints", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				LastSeen = knownEndpoint.LastSeen.Ticks,
				__document_id = knownEndpoint.__document_id
			});
		this.AddField("LastSeen");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("LastSeen.Ticks");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("LastSeen.Ticks");
		this.AddQueryParameterForReduce("__document_id");
	}
}
