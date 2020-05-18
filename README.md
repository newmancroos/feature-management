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
        Feature flage might be a <b>Enum</b>
    </li>
    <li>
        To enable/disble feature in a control we need to inject <b>IFeatureManager</b> and then we can write code within if block that check wether a feature enable/diable. <br>
        <img src=".\images\Feature_Check_Controller.JPG">
    </li>
</ul>
    
</p>
