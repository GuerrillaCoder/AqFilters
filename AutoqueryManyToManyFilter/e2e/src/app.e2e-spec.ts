import { AppPage } from './app.po';

describe('autoquery-many-to-many-filter App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('AutoqueryManyToManyFilter');
  });
});
