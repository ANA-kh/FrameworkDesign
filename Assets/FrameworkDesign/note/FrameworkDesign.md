##7
**交互逻辑**：用户操作或其他输入造成的逻辑  
**表现逻辑**：model数据变化后，更新view  
总结：
*	表现逻辑 适合用 事件 或 委托
*	表现逻辑用方法调用会造成很多问题，Controller 臃肿难维护、
*	Model 和 View 是自底向上的关系
*	自底向上用事件或委托
*	自顶向下用方法调用
*	Event 工具类不能传参
*	BindableProperty 是 数据 + 数据变更事件，可以节省代码量

##9
总结：
*	交互逻辑 会有很多 会让 Controller 臃肿
*	很多 Unity 框架的交互逻辑是由 Command 实现的
*	Command 模式可以让逻辑的调用和执行在空间和时间上分离
*	Command 分担 Controller 的交互逻辑
*	struct 比 class 有更好的内存管理效率


##21 完善IController
为接口创建abstract子类。  将样板代码统一在虚类中实现，避免具体子类的重复使用

##23 添加架构使用规则
使用explicit interface对类进行阉割，使对象无法直接使用接口，
然后使用Extension Methods来实现相关规则；继承该规则的类就可以使用相应接口

##24
实现基于类型的事件系统
通过手动添加MonoBehavior脚本（UnRegisterOnDestroyTrigger）实现在gameObject销毁时做一些事情
事件系统使用规则

##25  总节
**对象之间交互的三种方式**
*方法
*委托
*事件
**模块化的三种常规方式**
*单例
*IOC
*分层

**对象交互总结**
*自底向上用事件或委托
*自顶向下用方法调用

**一些理论**
*表现与逻辑分离
*交互逻辑（用户操作或其它触发的数据变动）与表现逻辑（）

## 29 贫血模型 与 充血模型
贫血模型： 数据交互时直接传递了存储数据用的对象，而此对象含有比需求更多的信息
充血模型： 只传递需要的信息

**充血模型实现方式：**
1. 根据需求在model层构造新对象，并提供查询接口
2. 新增查询系统层，在系统层调用model层，查询数据并构造新对象，并提供查询接口

## 30 总结
QFramework 系统设计架构分为四层：
*	表现层：ViewController 层
*	系统层：System 层
*	数据层：Model 层
*	工具层：Utility 层 

每个层级都有一些规则，如下：
表现层：
*	可以获取 System
*	可以获取 Model
*	可以发送 Command
*	可以监听 Event 

系统层：
*	可以获取 System
*	可以获取 Model
*	可以监听、发送 Event
*	可以获取 Utility  

数据层：
*	可以获取 Utility
*	可以发送 Event  

工具层：
*	啥都干不了，可以集成第三方库，或者封装 API。  

除了四个层级，还有一个核心概念 Command：
*	可以获取 System
*	可以获取 Model
*	可以发送 Event
*	可以获取 Utility
*	可以发送 Command
