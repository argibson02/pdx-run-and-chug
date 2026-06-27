import { expect, test } from '@playwright/test';

test('home page has expected heading', async ({ page }) => {
	await page.goto('/');
	await expect(page.getByRole('heading', { name: 'Next Location' })).toBeVisible();
});
