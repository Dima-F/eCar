Imports System.Text

<TestClass()>
Public Class UnitTest1

    Private testContextInstance As TestContext

    '''<summary>
    '''Получает или устанавливает контекст теста, в котором предоставляются
    '''сведения о текущем тестовом запуске и обеспечивается его функциональность.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Дополнительные атрибуты тестирования"
    '
    ' При написании тестов можно использовать следующие дополнительные атрибуты:
    '
    ' ClassInitialize используется для выполнения кода до запуска первого теста в классе
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' TestInitialize используется для выполнения кода перед запуском каждого теста
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' TestCleanup используется для выполнения кода после завершения каждого теста
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

    <TestMethod()>
    Public Sub TestMethod1()
        ' TODO: добавьте здесь логику теста
    End Sub

End Class
