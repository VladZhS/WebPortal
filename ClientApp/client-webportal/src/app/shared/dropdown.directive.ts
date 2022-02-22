import { Directive, HostBinding, HostListener } from "@angular/core";

@Directive(
    {
        selector: '[appDropDown]'
    }
)
export class DropDownDirective{
    @HostBinding('class.show') isOpen = false
    @HostListener('click') showDropDown(){
        this.isOpen = !this.isOpen
    }
}