using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeLounge.AOtomation.Messaging.Serialization.Serializers.Custom
{
    using System.Linq.Expressions;

    using SmokeLounge.AOtomation.Messaging.GameData;
    using SmokeLounge.AOtomation.Messaging.Messages.N3Messages;

    public class GenericCmdSerializer : ISerializer
    {
        private readonly Type type;
        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        public GenericCmdSerializer()
        {
            this.type = typeof(GenericCmdMessage);
        }

        public object Deserialize(
            StreamReader streamReader,
            SerializationContext serializationContext,
            PropertyMetaData propertyMetaData = null)
        {
            var mess = new GenericCmdMessage();
            mess.Temp1 = streamReader.ReadInt32();
            mess.Count = streamReader.ReadInt32();
            mess.Action = (GenericCmdAction)streamReader.ReadInt32();
            mess.Temp4 = streamReader.ReadInt32();
            mess.User = streamReader.ReadIdentity();
            int len = 1;
            if (mess.Action == GenericCmdAction.UseItemOnItem)
            {
                len = 2;
            }

            mess.Target=new Identity[len];
            for (int i = 0; i < mess.Target.Length; i++)
            {
                mess.Target[i] = streamReader.ReadIdentity();
            }
            return mess;
        }

        public Expression DeserializerExpression(
            ParameterExpression streamReaderExpression,
            ParameterExpression serializationContextExpression,
            Expression assignmentTargetExpression,
            PropertyMetaData propertyMetaData)
        {
            var deserializerMethodInfo =
    ReflectionHelper
        .GetMethodInfo
        <GenericCmdSerializer, Func<StreamReader, SerializationContext, PropertyMetaData, object>>
        (o => o.Deserialize);
            var serializerExp = Expression.New(this.GetType());
            var callExp = Expression.Call(
                serializerExp,
                deserializerMethodInfo,
                new Expression[]
                    {
                        streamReaderExpression, serializationContextExpression, 
                        Expression.Constant(propertyMetaData, typeof(PropertyMetaData))
                    });

            var assignmentExp = Expression.Assign(
                assignmentTargetExpression, Expression.TypeAs(callExp, assignmentTargetExpression.Type));
            return assignmentExp;
        }

        public void Serialize(
            StreamWriter streamWriter,
            SerializationContext serializationContext,
            object value,
            PropertyMetaData propertyMetaData = null)
        {
            var mess = (GenericCmdMessage)value;
            streamWriter.WriteInt32(mess.Temp1);
            streamWriter.WriteInt32(mess.Count);
            streamWriter.WriteInt32((int)mess.Action);
            streamWriter.WriteInt32(mess.Temp4);
            streamWriter.WriteIdentity(mess.User);
            foreach (Identity id in mess.Target)
            {
                streamWriter.WriteIdentity(id);
            }
        }

        public Expression SerializerExpression(
            ParameterExpression streamWriterExpression,
            ParameterExpression serializationContextExpression,
            Expression valueExpression,
            PropertyMetaData propertyMetaData)
        {
            var serializerMethodInfo =
    ReflectionHelper
        .GetMethodInfo
        <GenericCmdSerializer,
            Action<StreamWriter, SerializationContext, object, PropertyMetaData>>(o => o.Serialize);
            var serializerExp = Expression.New(this.GetType());
            var callExp = Expression.Call(
                serializerExp,
                serializerMethodInfo,
                new[]
                    {
                        streamWriterExpression, serializationContextExpression, valueExpression, 
                        Expression.Constant(propertyMetaData, typeof(PropertyMetaData))
                    });
            return callExp;
        }
    }
}
