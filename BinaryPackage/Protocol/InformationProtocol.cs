using System;

namespace BinaryPackage.Protocol
{
    public class InformationProtocol
    {
        /// <summary>
        /// Идендификационный номер 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Тип информации (входящая/исходящая)(0-исходящая, 1 - входящая)
        /// </summary>
        public TypeInformation InformationType { get; set; }

        /// <summary>
        /// Длина пакета
        /// </summary>
        public int PacketLength { get; set; }

        /// <summary>
        /// Тип протокола
        /// </summary>
        public ProtocolType ProtocolType { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public IBinary Body { get; set; }

        /// <summary>
        /// Дата/Время
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}