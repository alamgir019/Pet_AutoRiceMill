import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AppLayoutsComponent } from "./app-layouts/app-layouts.component";
import { RouterModule } from "@angular/router";
import { PrebuiltSkinsComponent } from "./prebuilt-skins/prebuilt-skins.component";

@NgModule({
  imports: [
    CommonModule,

    RouterModule.forChild([
      { path: "", redirectTo: "app-layouts", pathMatch: "full" },
      {
        path: "app-layouts",
        component: AppLayoutsComponent,
        // data: { state: "app-layouts" }
      },
      {
        path: "prebuilt-skins",
        component: PrebuiltSkinsComponent,
        // data: { state: "prebuilt-skins" }
      }
    ])
  ],
  declarations: [AppLayoutsComponent, PrebuiltSkinsComponent]
})
export class SmartadminIntelModule {}
