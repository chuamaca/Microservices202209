using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events
{
    //Un comando es un mensaje que se puede enviar desde uno o más remitentes y es procesado por un único receptor.
    //Un evento es un mensaje que se publica desde un único remitente y es procesado por(potencialmente) muchos receptores.

    // ------------------------------------------------------------------------
    //|                          | Comandos	             | Eventos             |
    //|------------------------------------------------------------------------
    //| Interfaz	             | ICommand	             | IEvent              |
    //| Remitentes               | lógicos	Uno o más	 | 1                   |
    //| Receptores Lógicos	     | 1	                 | Cero o más          |
    //| Propósito	             | "Por favor, haz algo" | "Algo ha pasado"    |
    //| Nombre (Tense)	         | Imperativo	         | Pasado              |
    //| Ejemplos	             | PlaceOrder            | OrderPlaced         |
    //|                          | ChargeCreditCard	     | CreditCardCharged   |
    //| Estilo de acoplamiento	 | Alto	                 | Bajo                |
    // ------------------------------------------------------------------------
    public class OrderPlaced: IEvent
    {
        public string OrderId { get; set; }
    }
}
