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

public class Transformer_MessagesViewTransformer : Raven.Database.Linq.AbstractTransformer
{
	public Transformer_MessagesViewTransformer()
	{
		this.ViewText = @"from message in results
where ((DateTime?)message.ProcessedAt) != null
select new {
	message = message,
	metadata = message.MessageMetadata
} into this0
select new {
	this0 = this0,
	headers = this0.message.Headers
} into this1
select new {
	this1 = this1,
	processedAt = this1.this0.message.ProcessedAt
} into this2
select new {
	this2 = this2,
	status = !((bool)(this2.this1.this0.message.MessageMetadata[""IsRetried""])) ? 3 : 4
} into this3
select new {
	Id = this3.this2.this1.this0.message.UniqueMessageId,
	MessageId = this3.this2.this1.this0.metadata[""MessageId""],
	MessageType = this3.this2.this1.this0.metadata[""MessageType""],
	SendingEndpoint = this3.this2.this1.this0.metadata[""SendingEndpoint""],
	ReceivingEndpoint = this3.this2.this1.this0.metadata[""ReceivingEndpoint""],
	TimeSent = ((DateTime?)this3.this2.this1.this0.metadata[""TimeSent""]),
	ProcessedAt = this3.this2.processedAt,
	CriticalTime = ((TimeSpan)this3.this2.this1.this0.metadata[""CriticalTime""]),
	ProcessingTime = ((TimeSpan)this3.this2.this1.this0.metadata[""ProcessingTime""]),
	DeliveryTime = ((TimeSpan)this3.this2.this1.this0.metadata[""DeliveryTime""]),
	IsSystemMessage = ((bool)this3.this2.this1.this0.metadata[""IsSystemMessage""]),
	ConversationId = this3.this2.this1.this0.metadata[""ConversationId""],
	Headers = this3.this2.this1.headers.Select(header => new System.Collections.Generic.KeyValuePair<string, object>(header.Key, header.Value)),
	Status = this3.status,
	MessageIntent = this3.this2.this1.this0.metadata[""MessageIntent""],
	BodyUrl = this3.this2.this1.this0.metadata[""BodyUrl""],
	BodySize = ((int)this3.this2.this1.this0.metadata[""ContentLength""]),
	InvokedSagas = this3.this2.this1.this0.metadata[""InvokedSagas""],
	OriginatesFromSaga = this3.this2.this1.this0.metadata[""OriginatesFromSaga""]
}
";
		this.TransformResultsDefinition = results => 
			from message in ((IEnumerable<dynamic>)results)
			where ((DateTime?)message.ProcessedAt) != null
			select new {
				message = message,
				metadata = message.MessageMetadata
			} into this0
			select new {
				this0 = this0,
				headers = this0.message.Headers
			} into this1
			select new {
				this1 = this1,
				processedAt = this1.this0.message.ProcessedAt
			} into this2
			select new {
				this2 = this2,
				status = !((bool)(this2.this1.this0.message.MessageMetadata["IsRetried"])) ? 3 : 4
			} into this3
			select new {
				Id = this3.this2.this1.this0.message.UniqueMessageId,
				MessageId = this3.this2.this1.this0.metadata["MessageId"],
				MessageType = this3.this2.this1.this0.metadata["MessageType"],
				SendingEndpoint = this3.this2.this1.this0.metadata["SendingEndpoint"],
				ReceivingEndpoint = this3.this2.this1.this0.metadata["ReceivingEndpoint"],
				TimeSent = ((DateTime?)this3.this2.this1.this0.metadata["TimeSent"]),
				ProcessedAt = this3.this2.processedAt,
				CriticalTime = ((TimeSpan)this3.this2.this1.this0.metadata["CriticalTime"]),
				ProcessingTime = ((TimeSpan)this3.this2.this1.this0.metadata["ProcessingTime"]),
				DeliveryTime = ((TimeSpan)this3.this2.this1.this0.metadata["DeliveryTime"]),
				IsSystemMessage = ((bool)this3.this2.this1.this0.metadata["IsSystemMessage"]),
				ConversationId = this3.this2.this1.this0.metadata["ConversationId"],
				Headers = this3.this2.this1.headers.Select((Func<dynamic, dynamic>)(header => new System.Collections.Generic.KeyValuePair<string, object>(header.Key, header.Value))),
				Status = this3.status,
				MessageIntent = this3.this2.this1.this0.metadata["MessageIntent"],
				BodyUrl = this3.this2.this1.this0.metadata["BodyUrl"],
				BodySize = ((int)this3.this2.this1.this0.metadata["ContentLength"]),
				InvokedSagas = this3.this2.this1.this0.metadata["InvokedSagas"],
				OriginatesFromSaga = this3.this2.this1.this0.metadata["OriginatesFromSaga"]
			};
	}
}
