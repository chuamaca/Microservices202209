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

public class Transformer_MessagesBodyTransformer : Raven.Database.Linq.AbstractTransformer
{
	public Transformer_MessagesBodyTransformer()
	{
		this.ViewText = @"from message in results
select new {
	message = message,
	metadata = message.MessageMetadata
} into this0
select new {
	MessageId = this0.metadata[""MessageId""],
	Body = !String.IsNullOrEmpty(this0.message.Body) ? this0.message.Body : this0.metadata[""Body""],
	BodySize = ((int)this0.metadata[""ContentLength""]),
	ContentType = this0.metadata[""ContentType""],
	BodyNotStored = ((bool)this0.metadata[""BodyNotStored""])
}
";
		this.TransformResultsDefinition = results => 
			from message in ((IEnumerable<dynamic>)results)
			select new {
				message = message,
				metadata = message.MessageMetadata
			} into this0
			select new {
				MessageId = this0.metadata["MessageId"],
				Body = !String.IsNullOrEmpty(this0.message.Body) ? this0.message.Body : this0.metadata["Body"],
				BodySize = ((int)this0.metadata["ContentLength"]),
				ContentType = this0.metadata["ContentType"],
				BodyNotStored = ((bool)this0.metadata["BodyNotStored"])
			};
	}
}
