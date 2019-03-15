var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define("ngx-popover/PopoverContent", ["require", "exports", "@angular/core"], function (require, exports, core_1) {
    "use strict";
    var PopoverContent = (function () {
        // -------------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------------
        function PopoverContent(element, cdr, renderer) {
            var _this = this;
            this.element = element;
            this.cdr = cdr;
            this.renderer = renderer;
            this.placement = "bottom";
            this.animation = true;
            this.closeOnClickOutside = false;
            this.closeOnMouseOutside = false;
            this.onCloseFromOutside = new core_1.EventEmitter();
            this.top = -10000;
            this.left = -10000;
            this.isIn = false;
            this.displayType = "none";
            // -------------------------------------------------------------------------
            // Anonymous 
            // -------------------------------------------------------------------------
            /**
             * Closes dropdown if user clicks outside of this directive.
             */
            this.onDocumentMouseDown = function (event) {
                var element = _this.element.nativeElement;
                if (!element || !_this.popover)
                    return;
                if (element.contains(event.target) || _this.popover.getElement().contains(event.target))
                    return;
                _this.hide();
                _this.onCloseFromOutside.emit(undefined);
            };
        }
        PopoverContent.prototype.ngAfterViewInit = function () {
            var _this = this;
            if (this.closeOnClickOutside)
                this.listenClickFunc = this.renderer.listenGlobal("document", "mousedown", function (event) { return _this.onDocumentMouseDown(event); });
            if (this.closeOnMouseOutside)
                this.listenMouseFunc = this.renderer.listenGlobal("document", "mouseover", function (event) { return _this.onDocumentMouseDown(event); });
            this.show();
            this.cdr.detectChanges();
        };
        PopoverContent.prototype.ngOnDestroy = function () {
            if (this.closeOnClickOutside)
                this.listenClickFunc();
            if (this.closeOnMouseOutside)
                this.listenMouseFunc();
        };
        // -------------------------------------------------------------------------
        // Public Methods
        // -------------------------------------------------------------------------
        PopoverContent.prototype.show = function () {
            if (!this.popover || !this.popover.getElement())
                return;
            var p = this.positionElements(this.popover.getElement(), this.popoverDiv.nativeElement, this.placement);
            this.displayType = "block";
            this.top = p.top;
            this.left = p.left;
            this.isIn = true;
        };
        PopoverContent.prototype.hide = function () {
            this.top = -10000;
            this.left = -10000;
            this.isIn = true;
            this.popover.hide();
        };
        PopoverContent.prototype.hideFromPopover = function () {
            this.top = -10000;
            this.left = -10000;
            this.isIn = true;
        };
        // -------------------------------------------------------------------------
        // Protected Methods
        // -------------------------------------------------------------------------
        PopoverContent.prototype.positionElements = function (hostEl, targetEl, positionStr, appendToBody) {
            if (appendToBody === void 0) { appendToBody = false; }
            var positionStrParts = positionStr.split("-");
            var pos0 = positionStrParts[0];
            var pos1 = positionStrParts[1] || "center";
            var hostElPos = appendToBody ? this.offset(hostEl) : this.position(hostEl);
            var targetElWidth = targetEl.offsetWidth;
            var targetElHeight = targetEl.offsetHeight;
            this.effectivePlacement = pos0 = this.getEffectivePlacement(pos0, hostEl, targetEl);
            var shiftWidth = {
                center: function () {
                    return hostElPos.left + hostElPos.width / 2 - targetElWidth / 2;
                },
                left: function () {
                    return hostElPos.left;
                },
                right: function () {
                    return hostElPos.left + hostElPos.width;
                }
            };
            var shiftHeight = {
                center: function () {
                    return hostElPos.top + hostElPos.height / 2 - targetElHeight / 2;
                },
                top: function () {
                    return hostElPos.top;
                },
                bottom: function () {
                    return hostElPos.top + hostElPos.height;
                }
            };
            var targetElPos;
            switch (pos0) {
                case "right":
                    targetElPos = {
                        top: shiftHeight[pos1](),
                        left: shiftWidth[pos0]()
                    };
                    break;
                case "left":
                    targetElPos = {
                        top: shiftHeight[pos1](),
                        left: hostElPos.left - targetElWidth
                    };
                    break;
                case "bottom":
                    targetElPos = {
                        top: shiftHeight[pos0](),
                        left: shiftWidth[pos1]()
                    };
                    break;
                default:
                    targetElPos = {
                        top: hostElPos.top - targetElHeight,
                        left: shiftWidth[pos1]()
                    };
                    break;
            }
            return targetElPos;
        };
        PopoverContent.prototype.position = function (nativeEl) {
            var offsetParentBCR = { top: 0, left: 0 };
            var elBCR = this.offset(nativeEl);
            var offsetParentEl = this.parentOffsetEl(nativeEl);
            if (offsetParentEl !== window.document) {
                offsetParentBCR = this.offset(offsetParentEl);
                offsetParentBCR.top += offsetParentEl.clientTop - offsetParentEl.scrollTop;
                offsetParentBCR.left += offsetParentEl.clientLeft - offsetParentEl.scrollLeft;
            }
            var boundingClientRect = nativeEl.getBoundingClientRect();
            return {
                width: boundingClientRect.width || nativeEl.offsetWidth,
                height: boundingClientRect.height || nativeEl.offsetHeight,
                top: elBCR.top - offsetParentBCR.top,
                left: elBCR.left - offsetParentBCR.left
            };
        };
        PopoverContent.prototype.offset = function (nativeEl) {
            var boundingClientRect = nativeEl.getBoundingClientRect();
            return {
                width: boundingClientRect.width || nativeEl.offsetWidth,
                height: boundingClientRect.height || nativeEl.offsetHeight,
                top: boundingClientRect.top + (window.pageYOffset || window.document.documentElement.scrollTop),
                left: boundingClientRect.left + (window.pageXOffset || window.document.documentElement.scrollLeft)
            };
        };
        PopoverContent.prototype.getStyle = function (nativeEl, cssProp) {
            if (nativeEl.currentStyle)
                return nativeEl.currentStyle[cssProp];
            if (window.getComputedStyle)
                return window.getComputedStyle(nativeEl)[cssProp];
            // finally try and get inline style
            return nativeEl.style[cssProp];
        };
        PopoverContent.prototype.isStaticPositioned = function (nativeEl) {
            return (this.getStyle(nativeEl, "position") || "static") === "static";
        };
        PopoverContent.prototype.parentOffsetEl = function (nativeEl) {
            var offsetParent = nativeEl.offsetParent || window.document;
            while (offsetParent && offsetParent !== window.document && this.isStaticPositioned(offsetParent)) {
                offsetParent = offsetParent.offsetParent;
            }
            return offsetParent || window.document;
        };
        PopoverContent.prototype.getEffectivePlacement = function (placement, hostElement, targetElement) {
            var placementParts = placement.split(" ");
            if (placementParts[0] !== "auto") {
                return placement;
            }
            var hostElBoundingRect = hostElement.getBoundingClientRect();
            var desiredPlacement = placementParts[1] || "bottom";
            if (desiredPlacement === "top" && hostElBoundingRect.top - targetElement.offsetHeight < 0) {
                return "bottom";
            }
            if (desiredPlacement === "bottom" && hostElBoundingRect.bottom + targetElement.offsetHeight > window.innerHeight) {
                return "top";
            }
            if (desiredPlacement === "left" && hostElBoundingRect.left - targetElement.offsetWidth < 0) {
                return "right";
            }
            if (desiredPlacement === "right" && hostElBoundingRect.right + targetElement.offsetWidth > window.innerWidth) {
                return "left";
            }
            return desiredPlacement;
        };
        return PopoverContent;
    }());
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], PopoverContent.prototype, "content", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], PopoverContent.prototype, "placement", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], PopoverContent.prototype, "title", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Boolean)
    ], PopoverContent.prototype, "animation", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Boolean)
    ], PopoverContent.prototype, "closeOnClickOutside", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Boolean)
    ], PopoverContent.prototype, "closeOnMouseOutside", void 0);
    __decorate([
        core_1.ViewChild("popoverDiv"),
        __metadata("design:type", core_1.ElementRef)
    ], PopoverContent.prototype, "popoverDiv", void 0);
    PopoverContent = __decorate([
        core_1.Component({
            selector: "popover-content",
            template: "\n<div #popoverDiv class=\"popover {{ effectivePlacement }}\"\n     [style.top]=\"top + 'px'\"\n     [style.left]=\"left + 'px'\"\n     [class.in]=\"isIn\"\n     [class.fade]=\"animation\"\n     style=\"display: block\"\n     role=\"popover\">\n    <div [hidden]=\"!closeOnMouseOutside\" class=\"virtual-area\"></div>\n    <div class=\"arrow\"></div> \n    <h3 class=\"popover-title\" [hidden]=\"!title\">{{ title }}</h3>\n    <div class=\"popover-content\">\n        <ng-content></ng-content>\n        {{ content }}\n    </div> \n</div>\n",
            styles: ["\n.popover .virtual-area {\n    height: 11px;\n    width: 100%;\n    position: absolute;\n}\n.popover.top .virtual-area {\n    bottom: -11px; \n}\n.popover.bottom .virtual-area {\n    top: -11px; \n}\n.popover.left .virtual-area {\n    right: -11px; \n}\n.popover.right .virtual-area {\n    left: -11px; \n}\n"]
        }),
        __metadata("design:paramtypes", [core_1.ElementRef,
            core_1.ChangeDetectorRef,
            core_1.Renderer])
    ], PopoverContent);
    exports.PopoverContent = PopoverContent;
});
define("ngx-popover/Popover", ["require", "exports", "@angular/core", "ngx-popover/PopoverContent"], function (require, exports, core_2, PopoverContent_1) {
    "use strict";
    var Popover = (function () {
        // -------------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------------
        function Popover(viewContainerRef, resolver) {
            this.viewContainerRef = viewContainerRef;
            this.resolver = resolver;
            // -------------------------------------------------------------------------
            // Properties
            // -------------------------------------------------------------------------
            this.PopoverComponent = PopoverContent_1.PopoverContent;
            this.popoverOnHover = false;
            this.popoverDismissTimeout = 0;
            this.onShown = new core_2.EventEmitter();
            this.onHidden = new core_2.EventEmitter();
        }
        // -------------------------------------------------------------------------
        // Event listeners
        // -------------------------------------------------------------------------
        Popover.prototype.showOrHideOnClick = function () {
            if (this.popoverOnHover)
                return;
            if (this.popoverDisabled)
                return;
            this.toggle();
        };
        Popover.prototype.showOnHover = function () {
            if (!this.popoverOnHover)
                return;
            if (this.popoverDisabled)
                return;
            this.show();
        };
        Popover.prototype.hideOnHover = function () {
            if (this.popoverCloseOnMouseOutside)
                return; // don't do anything since not we control this
            if (!this.popoverOnHover)
                return;
            if (this.popoverDisabled)
                return;
            this.hide();
        };
        Popover.prototype.ngOnChanges = function (changes) {
            if (changes["popoverDisabled"]) {
                if (changes["popoverDisabled"].currentValue) {
                    this.hide();
                }
            }
        };
        // -------------------------------------------------------------------------
        // Public Methods
        // -------------------------------------------------------------------------
        Popover.prototype.toggle = function () {
            if (!this.visible) {
                this.show();
            }
            else {
                this.hide();
            }
        };
        Popover.prototype.show = function () {
            var _this = this;
            if (this.visible)
                return;
            this.visible = true;
            if (typeof this.content === "string") {
                var factory = this.resolver.resolveComponentFactory(this.PopoverComponent);
                if (!this.visible)
                    return;
                this.popover = this.viewContainerRef.createComponent(factory);
                var popover = this.popover.instance;
                popover.popover = this;
                popover.content = this.content;
                if (this.popoverPlacement !== undefined)
                    popover.placement = this.popoverPlacement;
                if (this.popoverAnimation !== undefined)
                    popover.animation = this.popoverAnimation;
                if (this.popoverTitle !== undefined)
                    popover.title = this.popoverTitle;
                if (this.popoverCloseOnClickOutside !== undefined)
                    popover.closeOnClickOutside = this.popoverCloseOnClickOutside;
                if (this.popoverCloseOnMouseOutside !== undefined)
                    popover.closeOnMouseOutside = this.popoverCloseOnMouseOutside;
                popover.onCloseFromOutside.subscribe(function () { return _this.hide(); });
                // if dismissTimeout option is set, then this popover will be dismissed in dismissTimeout time
                if (this.popoverDismissTimeout > 0)
                    setTimeout(function () { return _this.hide(); }, this.popoverDismissTimeout);
            }
            else {
                var popover = this.content;
                popover.popover = this;
                if (this.popoverPlacement !== undefined)
                    popover.placement = this.popoverPlacement;
                if (this.popoverAnimation !== undefined)
                    popover.animation = this.popoverAnimation;
                if (this.popoverTitle !== undefined)
                    popover.title = this.popoverTitle;
                if (this.popoverCloseOnClickOutside !== undefined)
                    popover.closeOnClickOutside = this.popoverCloseOnClickOutside;
                if (this.popoverCloseOnMouseOutside !== undefined)
                    popover.closeOnMouseOutside = this.popoverCloseOnMouseOutside;
                popover.onCloseFromOutside.subscribe(function () { return _this.hide(); });
                // if dismissTimeout option is set, then this popover will be dismissed in dismissTimeout time
                if (this.popoverDismissTimeout > 0)
                    setTimeout(function () { return _this.hide(); }, this.popoverDismissTimeout);
                popover.show();
            }
            this.onShown.emit(this);
        };
        Popover.prototype.hide = function () {
            if (!this.visible)
                return;
            this.visible = false;
            if (this.popover)
                this.popover.destroy();
            if (this.content instanceof PopoverContent_1.PopoverContent)
                this.content.hideFromPopover();
            this.onHidden.emit(this);
        };
        Popover.prototype.getElement = function () {
            return this.viewContainerRef.element.nativeElement;
        };
        return Popover;
    }());
    __decorate([
        core_2.Input("popover"),
        __metadata("design:type", Object)
    ], Popover.prototype, "content", void 0);
    __decorate([
        core_2.Input(),
        __metadata("design:type", Boolean)
    ], Popover.prototype, "popoverDisabled", void 0);
    __decorate([
        core_2.Input(),
        __metadata("design:type", Boolean)
    ], Popover.prototype, "popoverAnimation", void 0);
    __decorate([
        core_2.Input(),
        __metadata("design:type", String)
    ], Popover.prototype, "popoverPlacement", void 0);
    __decorate([
        core_2.Input(),
        __metadata("design:type", String)
    ], Popover.prototype, "popoverTitle", void 0);
    __decorate([
        core_2.Input(),
        __metadata("design:type", Boolean)
    ], Popover.prototype, "popoverOnHover", void 0);
    __decorate([
        core_2.Input(),
        __metadata("design:type", Boolean)
    ], Popover.prototype, "popoverCloseOnClickOutside", void 0);
    __decorate([
        core_2.Input(),
        __metadata("design:type", Boolean)
    ], Popover.prototype, "popoverCloseOnMouseOutside", void 0);
    __decorate([
        core_2.Input(),
        __metadata("design:type", Number)
    ], Popover.prototype, "popoverDismissTimeout", void 0);
    __decorate([
        core_2.Output(),
        __metadata("design:type", Object)
    ], Popover.prototype, "onShown", void 0);
    __decorate([
        core_2.Output(),
        __metadata("design:type", Object)
    ], Popover.prototype, "onHidden", void 0);
    __decorate([
        core_2.HostListener("click"),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", []),
        __metadata("design:returntype", void 0)
    ], Popover.prototype, "showOrHideOnClick", null);
    __decorate([
        core_2.HostListener("focusin"),
        core_2.HostListener("mouseenter"),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", []),
        __metadata("design:returntype", void 0)
    ], Popover.prototype, "showOnHover", null);
    __decorate([
        core_2.HostListener("focusout"),
        core_2.HostListener("mouseleave"),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", []),
        __metadata("design:returntype", void 0)
    ], Popover.prototype, "hideOnHover", null);
    Popover = __decorate([
        core_2.Directive({
            selector: "[popover]",
            exportAs: "popover"
        }),
        __metadata("design:paramtypes", [core_2.ViewContainerRef,
            core_2.ComponentFactoryResolver])
    ], Popover);
    exports.Popover = Popover;
});
define("ngx-popover/index", ["require", "exports", "@angular/common", "ngx-popover/Popover", "ngx-popover/PopoverContent", "@angular/core", "ngx-popover/Popover", "ngx-popover/PopoverContent"], function (require, exports, common_1, Popover_1, PopoverContent_2, core_3, Popover_2, PopoverContent_3) {
    "use strict";
    function __export(m) {
        for (var p in m) if (!exports.hasOwnProperty(p)) exports[p] = m[p];
    }
    __export(Popover_2);
    __export(PopoverContent_3);
    var PopoverModule = (function () {
        function PopoverModule() {
        }
        return PopoverModule;
    }());
    PopoverModule = __decorate([
        core_3.NgModule({
            imports: [
                common_1.CommonModule
            ],
            declarations: [
                PopoverContent_2.PopoverContent,
                Popover_1.Popover,
            ],
            exports: [
                PopoverContent_2.PopoverContent,
                Popover_1.Popover,
            ],
            entryComponents: [
                PopoverContent_2.PopoverContent
            ]
        })
    ], PopoverModule);
    exports.PopoverModule = PopoverModule;
});
define("ngx-popover", ["require", "exports", "ngx-popover/index"], function (require, exports, index_1) {
    "use strict";
    function __export(m) {
        for (var p in m) if (!exports.hasOwnProperty(p)) exports[p] = m[p];
    }
    __export(index_1);
});
