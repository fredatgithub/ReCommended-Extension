using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework.Projects;
using NuGet.Frameworks;

namespace ReCommendedExtension.Tests;

// todo: remove when [TestNetFramework47] becomes available
// todo: also remove package reference "JetBrains.NuGet.Frameworks"

internal sealed class TestNetFramework47Attribute() : TestPlatformAttribute(FrameworkConstants.CommonFrameworks.Net47), ITestMsCorLibFlagProvider
{
    public ReferenceDlls GetMsCorLibFlag() => ReferenceDlls.MsCorLib;
}