

import {NgModule} from "@angular/core";

import {routing} from "./image-editor.routing";
import {ImageEditorComponent} from "./image-editor.component";
import { DefaultPanelComponent } from './default-panel/default-panel.component';
import { ApiPanelComponent } from './api-panel/api-panel.component';

// import rootReducer from './image-editor.reducer'
import {TabsModule} from "ngx-bootstrap";

import {ShowSelectionPanelComponent} from "./show-selection-panel/show-selection-panel.component";
import { PreviewPanelComponent } from './preview-panel/preview-panel.component';
import { AnimationsPanelComponent } from './animations-panel/animations-panel.component';
import { StylingPanelComponent } from './styling-panel/styling-panel.component';
import { JcropModule } from "@app/shared/forms/jcrop/jcrop.module";
import { SharedModule } from "@app/shared/shared.module";

@NgModule({
  imports: [routing,
    SharedModule, JcropModule, TabsModule],
  declarations: [ImageEditorComponent, DefaultPanelComponent, ApiPanelComponent, ShowSelectionPanelComponent, PreviewPanelComponent, AnimationsPanelComponent, StylingPanelComponent],
  exports: [ImageEditorComponent],
  providers: [],
})
export class ImageEditorModule{
  constructor(

  ) {
    // this.ngRedux.configureStore(
    //   rootReducer, {
    //     apiPanel:configJcropInitialState('apiPanel'),
    //     defaultPanel:configJcropInitialState('defaultPanel'),
    //     showSelectionPanel:configJcropInitialState('showSelectionPanel'),
    //     previewPanel:configJcropInitialState('previewPanel'),
    //     animationsPanel:configJcropInitialState('animationsPanel'),
    //     stylingPanel:configJcropInitialState('stylingPanel'),
    //   }
    // );
  }
}
