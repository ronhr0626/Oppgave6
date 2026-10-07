// ============================================================
// NOTE (Oppgave 6):
// The original program (What i did call Oppgave 5) only had Add, Get, GetAll and Count.
// To complete this test assignment i extended Repository<T> with two
// extra methods, Remove and Clear, because the assignment asks us to
// verify that Count changes correctly on Add, Remove and Clear.
// You cannot test a method that does not exist, so those two methods
// were added to the main program first, and are tested here.
//
// UPDATE after feedback:
// - GetAll now returns a COPY of the internal list, and a test verifies
//   that changing the returned list does not change the repository.
// - Data-driven tests with [Theory] + [InlineData] where the same logic
//   is tested with several values.
// ============================================================

using TrollMixCatalog;

namespace Tests;

// Tests for the generic Repository<T> class.
// We use small, controlled data in each test so we know exactly
// what should happen (Arrange - Act - Assert pattern).
public class RepositoryTests
{
    // ---------- A. BASIC FUNCTIONALITY ----------

    [Fact]
    public void Add_IncreasesCount_And_ItemIsRetrievable()
    {
        // Arrange
        var repo = new Repository<Song>();
        var song = new Song("Fløybanene", "Norwegian", "Visepop", 2026);

        // Act
        repo.Add(song);

        // Assert
        Assert.Equal(1, repo.Count);
        Assert.Same(song, repo.Get(0));
    }

    [Fact]
    public void Remove_RemovesTheItalianSong_And_DecreasesCount()
    {
        // Arrange
        var repo = new Repository<Song>();
        var norwegian = new Song("Så la det regne", "Norwegian", "Pop", 2026);
        var italian = new Song("Quaranta gradi", "Italian", "Pop", 2026);
        repo.Add(norwegian);
        repo.Add(italian);

        // Act
        bool removed = repo.Remove(italian);

        // Assert
        Assert.True(removed);
        Assert.Equal(1, repo.Count);
        Assert.DoesNotContain(italian, repo.GetAll());
        Assert.Contains(norwegian, repo.GetAll());
    }

    [Fact]
    public void Clear_EmptiesTheRepository()
    {
        // Arrange
        var repo = new Repository<Song>();
        repo.Add(new Song("Bailando contigo", "Spanish", "Latin", 2026));
        repo.Add(new Song("Herz aus Code", "German", "Disco inspired", 2026));

        // Act
        repo.Clear();

        // Assert
        Assert.Equal(0, repo.Count);
        Assert.Empty(repo.GetAll());
    }

    [Fact]
    public void GetAll_ModifyingReturnedList_DoesNotChangeRepository()
    {
        // Arrange
        var repo = new Repository<string>();
        repo.Add("2025");
        repo.Add("2026");

        // Act - try to change the repository from the outside
        var list = repo.GetAll();
        list.Clear();
        list.Add("hacked");

        // Assert - the repository still has its original items
        Assert.Equal(2, repo.Count);
        Assert.Equal("2025", repo.Get(0));
        Assert.Equal("2026", repo.Get(1));
    }

    // ---------- B. GENERIC CONTRACT (works with other types) ----------

    [Theory]
    [InlineData("2025")]
    [InlineData("2026")]
    public void Repository_WorksWithStrings_NotJustSongs(string value)
    {
        // Arrange
        var repo = new Repository<string>();

        // Act
        repo.Add(value);

        // Assert
        Assert.Equal(1, repo.Count);
        Assert.Equal(value, repo.Get(0));
    }

    // ---------- C. EDGE CASES ----------

    [Theory]
    [InlineData(-1)]
    [InlineData(99)]
    public void Get_WithInvalidIndex_ThrowsException(int index)
    {
        // Arrange
        var repo = new Repository<Song>();
        repo.Add(new Song("Talk with my ai", "English", "Synth-pop", 2026));

        // Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => repo.Get(index));
    }
}