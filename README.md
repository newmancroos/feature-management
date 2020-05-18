# feature-management

<p>
    Feature management in .Net or .Net Core gives flexibility to On/Off feature to a .Net application thru configuration file or Azure configuration. We can On/Off feature to a application without build the application that means even on the production with just configuration file or azure configuration change.
</p>
<p>
    <ul>
        <li>
            To work with this featuer we need to install a Nuget package called <b>Microsoft.FeatureManagement.AspNetCore</b>
        </li>
        <li>
            Create a Feature flag to indiacate the name of the features and later we could use this flage to On/Off the feature.
            Feature flage might be a <b>Enum</b> enum name should be <b>FeatureFlage</b> <br>
            <img src=".\images\enum.JPG">
            <br>
        </li>
        <li>
            To enable/disble feature in a control we need to inject <b>IFeatureManager</b> and then we can write code within if block that check wether a feature enable/diable. <br><br>
            <img src=".\images\Feature_Check_Controller.JPG"><br>
        </li>
        <li>
            We should register <b>FeatureManagement</b> in the startup.cs to work correctly.
            <pre>
                services.AddFeatureManagement();
            </pre>
            <br>
            Now if we run the project we'll get error on the view because in controller since we haven't yet enable or On the <b>FeatureFlage -&gt; ListEmployee</b> in in controller fetching employee will be skipped and in view employees object is null.
            So we need to check the feature wether enable or not.
        </li>
        <li>
            Implementing Feature Management check in the view we need to first register the Tag Helper to <b>_ViewImport.cshtml</b>
            <img src=".\images\FeatureManagement_TagHelper_Import.JPG">
        </li>
        <li>
            now we can use the Feature check in the view
            <img src=".\images\Feature_Check_in_View.JPG">
        </li>
        <li>
            Now we know how to define and use <b>Feature Management</b> but how to Endble/On the feature??????
        </li>
        <li>
            We can declare the Feature Management in <b>AppSetting.json or Azure Feature management configuration</b>
        </li>
        <li>
            Declare Feature management in appSetting.json under <b>FeatureManagement -- [FeatureName : true]</b><br>
            <img src=".\images\Enable_Feature_AppSetting.JPG">
        </li>
        <li>
            we can configure the feature Date and Time basis, Claims basis and some other criteria basis.
        </li>
        <li>
            <h2>Configuring a Feature flage that enabled for certain date and time [TimeWindowFilter]</h2><br>
            We have another <b>Feature Flag named ListUsers</b> we are going to setup List User for a date and time<br>
            <ol>
                <li>There are Feature filter we can enable it during the registration of the Feature Management in the startup.cs Configuration method<br>
                <pre>
                        services.AddFeatureManagement()
                            .AddFeatureFilter&lt;TimeWindowFilter&gt;();
                </pre>
                </li>
                <li>
                    Specify the Time configuration in thw appsetting.json<br>
                    <img src=".\images\TimeWindowSetting.JPG"><br><br>
                    Now if yo run the application and try to list User then it will not listdown the user because the date and time already expires. If you change the date/time interval with current date time you will see the user list
                </li>
            </ol>
        </li>
        <li>
            <h2>Configuring a Feature flage that enabled for feature by Percentage [PercentageFilter]</h2><br>
                <ol>
                    <li>
                        <pre>
                            services.AddFeatureManagement()
                                .AddFeatureFilter&lt;PercentageFilter&gt;();
                        </pre>
                    </li>
                    <li>
                        In the appSetting we have to specify the percentage of filter has to be shown that means if you give 50 then for some request it will return the response for some not return. (On for 50 percentage Off for 50 percentage)
                        <img src=".\images\PercentageFilter.JPG"><br><br>
                        <img src=".\images\ThreeFliterEnum.jpg">
                        <img src=".\images\TwoFilterStartup.jpg">
                    </li>
                    <li>
                        In our sample application, Percentage sample may gives error because Filter is enable during Controller method but not when View render.( we are checking in both places). We can correct it by injecting <b>IFeatureManagerSnapshot</b> instead of <b>IFeatureManger</b> and No need to change in private readonly variable in class level.
                        What is does is <b>Maintaining the same Feature Ob/Off status for current request life time</b>
                    </li>
                </ol>
        </li>
    </ul>
    <p>
        There are many other Feature filter there for categorize intenal and external user ... etc.
    </p>
    <hr/>
    <p>
        We can have over Feature Management configuration in <b>Azure Configuration</b> instead of <b>AppSetting.json</b>
    </p>
    <p>
        <h2>Feature Management in Azure</h2><br>
        <ol>
            <li>
                Create a App Configuration in Azure <br>
                * Search for App Configuration <br>
                <img src=".\images\azure-portal-search.png"><br><br>
                * Create <b>App Configuration</b><br>
                <img src=".\images\app-configuration-create.png"><br><br>
                * Fill all the fields<br>
                <img src=".\images\app-configuration-create-settings.png"> <br><br>
                *Deplyment takes place and create a App Configuratio in the given name. <br>
                *Open your App configuration and Add a feature flage<br>
                <img src=".\images\add-beta-feature-flag.png"> <br><br>
                *Note down the connection string for the created App configuration<br>
            </li>
        </ol>
    </p>

</p>
