using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[AttributeUsage(
    AttributeTargets.Class |
    AttributeTargets.Enum |
    AttributeTargets.Struct)]
public class NotificationMessageAttribute : Attribute
{

    private readonly string m_notificationClassDirectory;

    private Type m_notificationMessageType;

    //--------------------------------------------------------------------------

    public string notificationClassDirectory => m_notificationClassDirectory;

    public Type notificationMessageType => m_notificationMessageType;

    //--------------------------------------------------------------------------

    public NotificationMessageAttribute(string notificationClassDirectory)
    {
        m_notificationClassDirectory = notificationClassDirectory;
    }

    //--------------------------------------------------------------------------

    public static IEnumerable<NotificationMessageAttribute> FindAll()
    {
        var thisType = typeof(NotificationMessageAttribute);
        var thisAssembly = thisType.Assembly;
        var thisAssemblyFullName = thisAssembly.GetName().FullName;

        var candidateAssemblies =
            AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Where(assembly =>
                assembly
                .GetReferencedAssemblies()
                .Any(referencedAssembly =>
                    referencedAssembly.FullName == thisAssemblyFullName));

        foreach (var assembly in candidateAssemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                var attribute =
                    (NotificationMessageAttribute)
                    GetCustomAttribute(type, thisType);
                if (attribute != null)
                {
                    attribute.m_notificationMessageType = type;
                    yield return attribute;
                }
            }
        }
    }

}