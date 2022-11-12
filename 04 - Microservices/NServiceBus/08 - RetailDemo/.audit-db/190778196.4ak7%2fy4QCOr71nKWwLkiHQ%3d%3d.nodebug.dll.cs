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

public class Index_MessagesViewIndex : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_MessagesViewIndex()
	{
		this.ViewText = @"from message in docs.ProcessedMessages
select new {
	MessageId = ((string)message.MessageMetadata[""MessageId""]),
	MessageType = ((string)message.MessageMetadata[""MessageType""]),
	IsSystemMessage = ((bool)message.MessageMetadata[""IsSystemMessage""]),
	Status = ((bool)message.MessageMetadata[""IsRetried""]) ? 4 : 3,
	TimeSent = ((DateTime)message.MessageMetadata[""TimeSent""]),
	ProcessedAt = message.ProcessedAt,
	ReceivingEndpointName = ((message.MessageMetadata[""ReceivingEndpoint""])).Name,
	CriticalTime = ((TimeSpan?)message.MessageMetadata[""CriticalTime""]),
	ProcessingTime = ((TimeSpan?)message.MessageMetadata[""ProcessingTime""]),
	DeliveryTime = ((TimeSpan?)message.MessageMetadata[""DeliveryTime""]),
	Query = Enumerable.ToArray(DynamicEnumerable.Union(message.MessageMetadata.Select(_ => _.Value.ToString()), new string[] {
		String.Join("" "", message.Headers.Select(x => x.Value))
	})),
	ConversationId = ((string)message.MessageMetadata[""ConversationId""])
}";
		this.ForEntityNames.Add("ProcessedMessages");
		this.AddMapDefinition(docs => 
			from message in ((IEnumerable<dynamic>)docs)
			where string.Equals(message["@metadata"]["Raven-Entity-Name"], "ProcessedMessages", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				MessageId = ((string)message.MessageMetadata["MessageId"]),
				MessageType = ((string)message.MessageMetadata["MessageType"]),
				IsSystemMessage = ((bool)message.MessageMetadata["IsSystemMessage"]),
				Status = ((bool)message.MessageMetadata["IsRetried"]) ? 4 : 3,
				TimeSent = ((DateTime)message.MessageMetadata["TimeSent"]),
				ProcessedAt = message.ProcessedAt,
				ReceivingEndpointName = ((message.MessageMetadata["ReceivingEndpoint"])).Name,
				CriticalTime = ((TimeSpan?)message.MessageMetadata["CriticalTime"]),
				ProcessingTime = ((TimeSpan?)message.MessageMetadata["ProcessingTime"]),
				DeliveryTime = ((TimeSpan?)message.MessageMetadata["DeliveryTime"]),
				Query = Enumerable.ToArray(DynamicEnumerable.Union(message.MessageMetadata.Select((Func<dynamic, dynamic>)(_ => _.Value.ToString())), new string[] {
					String.Join(" ", message.Headers.Select((Func<dynamic, dynamic>)(x => x.Value)))
				})),
				ConversationId = ((string)message.MessageMetadata["ConversationId"]),
				__document_id = message.__document_id
			});
		this.AddField("MessageId");
		this.AddField("MessageType");
		this.AddField("IsSystemMessage");
		this.AddField("Status");
		this.AddField("TimeSent");
		this.AddField("ProcessedAt");
		this.AddField("ReceivingEndpointName");
		this.AddField("CriticalTime");
		this.AddField("ProcessingTime");
		this.AddField("DeliveryTime");
		this.AddField("Query");
		this.AddField("ConversationId");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("ProcessedAt");
		this.AddQueryParameterForMap("Name");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("ProcessedAt");
		this.AddQueryParameterForReduce("Name");
		this.AddQueryParameterForReduce("__document_id");
	}
}
