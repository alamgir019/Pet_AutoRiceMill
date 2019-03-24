
import {ModuleWithProviders} from "@angular/core"
import {RouterModule, Routes} from "@angular/router";


export const routes:Routes = [
  { path: 'editors',
    loadChildren: './bootstrap-editors/bootstrap-editors.module#BootstrapEditorsModule',
    data: {pageTitle: 'Bootstrap Editors'}
  },

  { path: 'bootstrap-elements',
    loadChildren: './bootstrap-elements/bootstrap-elements.module#BootstrapElementsModule',
    data: {pageTitle: 'Bootstrap Elements'}
  },

  {
    path: 'bootstrap-validation',
    loadChildren: './bootstrap-validation/bootstrap-validation.module#BootstrapValidationModule',
    data: {pageTitle: 'Bootstrap Validation'}
  },
  {
    path: 'dropzone',
    loadChildren: './dropzone-showcase/dropzone-showcase.module#DropzoneShowcaseModule',
    data: {pageTitle: 'Dropzone'}
  },
  {
    path: 'elements',
    loadChildren: './form-elements/form-elements.module#FormElementsModule',
    data: {pageTitle: 'Elements'}
  },
  {
    path: 'layouts',
    loadChildren: './form-layouts/form-layouts.module#FormLayoutsModule',
    data: {pageTitle: 'Layouts'}
  },
  {
    path: 'plugins',
    loadChildren: './form-plugins/form-plugins.module#FormPluginsModule',
    data: {pageTitle: 'Plugins'}
  },
  {
    path: 'validation',
    loadChildren: './form-validation/form-validation.module#FormValidationModule',
    data: {pageTitle: 'Validation'}
  },

  {
    path: 'wizards',
    loadChildren: './wizards/wizards.module#WizardsModule',
    data: {pageTitle: 'Wizards'}
  },
];

export const routing = RouterModule.forChild(routes);
