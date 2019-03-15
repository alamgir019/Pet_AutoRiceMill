"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var PopoverContent_1 = require("./PopoverContent");
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
        this.onShown = new core_1.EventEmitter();
        this.onHidden = new core_1.EventEmitter();
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
    core_1.Input("popover"),
    __metadata("design:type", Object)
], Popover.prototype, "content", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Boolean)
], Popover.prototype, "popoverDisabled", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Boolean)
], Popover.prototype, "popoverAnimation", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", String)
], Popover.prototype, "popoverPlacement", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", String)
], Popover.prototype, "popoverTitle", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Boolean)
], Popover.prototype, "popoverOnHover", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Boolean)
], Popover.prototype, "popoverCloseOnClickOutside", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Boolean)
], Popover.prototype, "popoverCloseOnMouseOutside", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Number)
], Popover.prototype, "popoverDismissTimeout", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], Popover.prototype, "onShown", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], Popover.prototype, "onHidden", void 0);
__decorate([
    core_1.HostListener("click"),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], Popover.prototype, "showOrHideOnClick", null);
__decorate([
    core_1.HostListener("focusin"),
    core_1.HostListener("mouseenter"),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], Popover.prototype, "showOnHover", null);
__decorate([
    core_1.HostListener("focusout"),
    core_1.HostListener("mouseleave"),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], Popover.prototype, "hideOnHover", null);
Popover = __decorate([
    core_1.Directive({
        selector: "[popover]",
        exportAs: "popover"
    }),
    __metadata("design:paramtypes", [core_1.ViewContainerRef,
        core_1.ComponentFactoryResolver])
], Popover);
exports.Popover = Popover;
//# sourceMappingURL=Popover.js.map