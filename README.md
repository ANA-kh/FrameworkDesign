# FrameworkDesign

#### 介绍
 学习开源框架QFramework并进行的实践

####  架构分为四个层级:
-	表现层：IController 接口，负责接收输入和当状态变化时更新表现，一般情况下 MonoBehaviour 均为表现层对象。
-	系统层：ISystem 接口，帮助 IController 承担一部分逻辑，在多个表现层共享的逻辑，比如计时系统、商城系统、成就系统等。
-	模型层：IModel 接口，负责数据的定义以及数据的增删改查方法的的提供。
-	工具层：IUtility 接口，负责提供基础设施，比如存储方法、序列化方法等。  

####  使用规则：
-	IController 更改 ISystem、IModel 的状态必须用 Command。
-	ISystem、IModel 状态发生变更后通知 IController 必须用事件 或 BindableProeprty。
-	IController 可以获取 ISystem、IModel 对象来进行数据查询。
-	ICommand 不能有状态。
-	上层可以直接获取下层对象，下层不能获取上层对象。
-	下层像上层通信用事件。
-	上层向下层通信用方法调用。
