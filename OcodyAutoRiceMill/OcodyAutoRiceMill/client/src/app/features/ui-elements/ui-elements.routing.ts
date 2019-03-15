import { ModuleWithProviders } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

export const routes: Routes = [
  {
    path: "",
    redirectTo: "buttons",
    pathMatch: "full"
  },
  {
    path: "buttons",
    loadChildren: "./buttons/buttons.module#ButtonsModule",
    data: { pageTitle: "Buttons" }
  },
  {
    path: "grid",
    loadChildren: "./grid/grid.module#GridModule",
    data: { pageTitle: "Grid" }
  },
  {
    path: "typography",
    loadChildren: "./typography/typography.module#TypographyModule",
    data: { pageTitle: "Typography" }
  },
  {
    path: "tree-views",
    loadChildren: "./tree-views/tree-views.module#TreeViewsModule",
    data: { pageTitle: "Tree Views" }
  },
  {
    path: "nestable-lists",
    loadChildren: "./nestable-lists/nestable-lists.module#NestableListsModule",
    data: { pageTitle: "Nestable Lists" }
  },
  {
    path: "general",
    loadChildren:
      "./general-elements/general-elements.module#GeneralElementsModule",
    data: { pageTitle: "General Elements" }
  },
  {
    path: "icons",
    loadChildren: "./icons/icons.module#IconsModule",
    data: { pageTitle: "Icons" }
  }
];

export const routing = RouterModule.forChild(routes);
