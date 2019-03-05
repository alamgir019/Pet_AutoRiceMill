import { AutoRiceMillTemplatePage } from './app.po';

describe('AutoRiceMill App', function() {
  let page: AutoRiceMillTemplatePage;

  beforeEach(() => {
    page = new AutoRiceMillTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
